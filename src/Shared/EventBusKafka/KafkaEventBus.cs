namespace EventBusKafka;

using System;
using System.Diagnostics;
using System.Threading;
using Avro;
using Avro.Generic;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using Polly.Retry;

public sealed class KafkaEventBus(
    ILogger<KafkaEventBus> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusOptions> options,
    IOptions<EventBusSubscriptionInfo> subscriptionOptions,
    KafkaTelemetry kafkaTelemetry,
    IProducer<string, GenericRecord> kafkaProducer,
    IConsumer<string, GenericRecord> kafkaConsumer) : IEventBus, IDisposable, IHostedService
{
    private readonly ResiliencePipeline _pipeline = CreateResiliencePipeline(options.Value.RetryCount);
    private readonly TextMapPropagator _propagator = kafkaTelemetry.Propagator;
    private readonly ActivitySource _activitySource = kafkaTelemetry.ActivitySource;
    private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionOptions.Value;
    private readonly IProducer<string, GenericRecord> _kafkaProducer = kafkaProducer;
    private IConsumer<string, GenericRecord> _kafkaConsumer = kafkaConsumer;

    #region Producer
    public Task PublishAsync(IntegrationEvent @event)
    {
        if (_kafkaProducer == null)
        {
            throw new InvalidOperationException("Kafka producer is not enabled.");
        }

        var topic = @event.GetType().Name.ToLowerInvariant();
        var name = @event.GetType().Name;
        var nameSpace = @event.GetType().Namespace;

        if (nameSpace == null)
        {
            throw new ArgumentException("The integration event {EventId} does not have a valid namespace.");
        }

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Creating Kafka producer to publish event: {EventId} ({EventName})", @event.IntegrationEventId, topic);
        }

        // Start an activity with a name following the semantic convention of the OpenTelemetry messaging specification.
        var activityName = $"{topic} publish";

        return _pipeline.Execute(async () =>
        {
            using var activity = _activitySource.StartActivity(activityName, ActivityKind.Client);

            ActivityContext contextToInject = default;

            if (activity != null)
            {
                contextToInject = activity.Context;
            }
            else if (Activity.Current != null)
            {
                contextToInject = Activity.Current.Context;
            }

            var headers = new Headers();
            _propagator.Inject(new PropagationContext(contextToInject, Baggage.Current), headers, (headers, key, value) => headers.Add(key, Encoding.UTF8.GetBytes(value)));

            SetActivityContext(activity, topic, "publish");

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Publishing event to Kafka: {EventId}", @event.IntegrationEventId);
            }

            try
            {
                var message = CreateKafkaMessage(@event, name, nameSpace);
                var deliveryResult = await _kafkaProducer.ProduceAsync(topic, message);

                if (deliveryResult.Status == PersistenceStatus.Persisted)
                {
                    logger.LogInformation("Delivered message to {TopicPartitionOffset}", deliveryResult.TopicPartitionOffset);
                }
                else
                {
                    logger.LogWarning("Message delivery not guaranteed: {Status}", deliveryResult.Status);
                }
            }
            catch (Exception ex)
            {
                activity.SetExceptionTags(ex);
                throw;
            }
        });
    }

    public void Dispose()
    {
        _kafkaProducer?.Dispose();
    }

    private Message<string, GenericRecord> CreateKafkaMessage(IntegrationEvent @event, string name, string nameSpace)
    {
        var fields = new List<string>();
        foreach (var prop in @event.GetType().GetProperties())
        {
            string fieldType;
            switch (prop.PropertyType)
            {
                case Type t when t == typeof(int):
                    fieldType = "int";
                    break;
                case Type t when t == typeof(bool):
                    fieldType = "boolean";
                    break;
                case Type t when t == typeof(string):
                    fieldType = "string";
                    break;
                /*case Type t when t == typeof(object) && prop.GetValue(null) == null:
                    fieldType = "null";
                    break;*/
                default:
                    fieldType = "string";
                    break;
            }

            var value = prop.GetValue(@event);
            if (value == null)
            {
                fields.Add($@"{{ ""name"": ""{prop.Name}"", ""type"": [""null"", ""{fieldType}""] }}");
            }
            else
            {
                fields.Add($@"{{ ""name"": ""{prop.Name}"", ""type"": ""{fieldType}"" }}");
            }
        }

        var schemaString = $@"{{
            ""type"": ""record"",
            ""name"": ""{name}"",
            ""namespace"": ""{nameSpace}"",
            ""fields"": [
                {string.Join(",", fields)}
            ]
        }}";

        var schema = (RecordSchema)Avro.Schema.Parse(schemaString);
        var record = new GenericRecord(schema);

        // Add @event values to GenericRecord
        foreach (var prop in @event.GetType().GetProperties())
        {
            var value = prop.GetValue(@event);
            switch (value)
            {
                case int intValue:
                    record.Add(prop.Name, intValue);
                    break;
                case bool boolValue:
                    record.Add(prop.Name, boolValue);
                    break;
                case null:
                    record.Add(prop.Name, null);
                    break;
                default:
                    record.Add(prop.Name, value?.ToString());
                    break;
            }
        }

        // Create key, get the value of the Id property of the integration event
        var key = @event.GetType().GetProperty("IntegrationEventId")?.GetValue(@event)?.ToString();
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("The integration event does not have a valid 'IntegrationEventId' property.");
        }

        return new Message<string, GenericRecord> { Key = key, Value = record };
    }

    #endregion Producer

    #region Consumer
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var options = serviceProvider.GetRequiredService<IOptions<EventBusOptions>>().Value;

        if (_kafkaConsumer != null)
        {
            try
            {
                _kafkaConsumer.Subscribe(_subscriptionInfo.EventTypes.Keys.Select(key => key.ToLowerInvariant()));

                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            try
                            {
                                var consumeResult = _kafkaConsumer.Consume(cancellationToken);
                                if (consumeResult != null)
                                {
                                    Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");

                                    var eventName = consumeResult.Message.Value.Schema.Name;

                                    if (!_subscriptionInfo.EventTypes.TryGetValue(eventName, out var eventType))
                                    {
                                        logger.LogWarning("Unable to resolve event type for event name {EventName}", eventName);
                                        return;
                                    }

                                    // Deserializar mensaje en una instancia del tipo de evento
                                    var eventData = DeserializeMessage(eventType, consumeResult.Message.Value);
                                    if (eventData == null)
                                    {
                                        logger.LogWarning("Failed to deserialize event of type {EventType}", eventType.Name);
                                        return;
                                    }

                                    // Procesar el evento
                                    await ProcessEvent(eventType, eventData);
                                }
                                else
                                {
                                    logger.LogWarning("No message received from Kafka.");
                                }
                            }
                            catch (ConsumeException ex)
                            {
                                logger.LogError(ex, "Error consuming Kafka message: {ErrorCode}", ex.Error.Code);
                            }
                            catch (OperationCanceledException)
                            {
                                // Ignorar cancelación
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Unexpected error consuming Kafka message");
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignorar cancelación
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error in Kafka consumer loop");
                    }
                }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error StartAsync Kafka");
            }
        }
        else
        {
            logger.LogWarning("Kafka consumer configuration is incomplete. Consumer will not be started.");
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _kafkaConsumer?.Close();
        return Task.CompletedTask;
    }

    private object DeserializeMessage(Type eventType, GenericRecord messageValue)
    {
        var eventDataDict = new Dictionary<string, object>();
        foreach (var field in messageValue.Schema.Fields)
        {
            if (messageValue.TryGetValue(field.Name, out var value))
            {
                eventDataDict[field.Name] = value;
            }
        }

        // Deserializar el diccionario en una instancia del tipo de evento
        var eventDataJson = JsonConvert.SerializeObject(eventDataDict);
        return JsonConvert.DeserializeObject(eventDataJson, eventType);
    }

    private async Task ProcessEvent(Type eventType, object eventData)
    {
        //crear ambito puede resolver cualquier servicio registrado en el contenedor de dependencias
        await using var scope = serviceProvider.CreateAsyncScope();
        //obtener instancias de servicios que tienen IIntegrationEventHandler registrados con nombre eventType
        foreach (var handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
        {
            if (handler is not null && eventData is not null)
            {
                await handler.Handle((IntegrationEvent)eventData);
            }
            else
            {
                logger.LogWarning("Handler or eventData is null for {EventType}", eventType.Name);
                return;
            }
        }
    }
    #endregion Consumer

    private static ResiliencePipeline CreateResiliencePipeline(int retryCount)
    {
        // See https://www.pollydocs.org/strategies/retry.html
        var retryOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<KafkaException>().Handle<SocketException>(),
            MaxRetryAttempts = retryCount,
            DelayGenerator = (context) => ValueTask.FromResult(GenerateDelay(context.AttemptNumber))
        };

        return new ResiliencePipelineBuilder()
            .AddRetry(retryOptions)
            .Build();

        static TimeSpan? GenerateDelay(int attempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, attempt));
        }
    }

    private static void SetActivityContext(Activity activity, string topic, string operation)
    {
        if (activity is not null)
        {
            // Estas etiquetas se añaden siguiendo las convenciones semánticas de la especificación de OpenTelemetry para mensajería
            // https://github.com/open-telemetry/semantic-conventions/blob/main/docs/messaging/messaging-spans.md
            activity.SetTag("messaging.system", "kafka");
            activity.SetTag("messaging.destination_kind", "topic");
            activity.SetTag("messaging.operation", operation);
            activity.SetTag("messaging.destination.name", topic);
            activity.SetTag("messaging.kafka.topic", topic);
        }
    }
}
