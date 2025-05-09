using Avro.Generic;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using EventBusKafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using Confluent.Kafka.SyncOverAsync;

namespace Microsoft.Extensions.Hosting;

public static class KafkaDependencyInjectionExtensions
{
    private const string SectionName = "EventBus";

    public static IEventBusBuilder AddKafkaEventBus(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // Configurar opciones
        builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection(SectionName));

        // Configurar productor de Kafka
        builder.Services.AddSingleton<IProducer<string, GenericRecord>>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<EventBusOptions>>().Value;
            if (string.IsNullOrEmpty(options.Username) || string.IsNullOrEmpty(options.Password))
            {
                return null; // No crear el productor si no hay productor configurado
            }
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = options.BootstrapServers,
                SaslUsername = options.Username,
                SaslPassword = options.Password,
                SaslMechanism = SaslMechanism.ScramSha512,
                SecurityProtocol = SecurityProtocol.SaslPlaintext
            };
            var schemaRegistryConfig = new SchemaRegistryConfig { Url = options.SchemaRegistryUrl };
            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            var producer = new ProducerBuilder<string, GenericRecord>(producerConfig)
                .SetValueSerializer(new AvroSerializer<GenericRecord>(schemaRegistry))
                .Build();
            return producer;
        });

        //Configurar consumidor de Kafka
        builder.Services.AddSingleton<IConsumer<string, GenericRecord>>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<EventBusOptions>>().Value;
            if (string.IsNullOrEmpty(options.GroupId) || string.IsNullOrEmpty(options.ConsumerUsername) || string.IsNullOrEmpty(options.ConsumerPassword))
            {
                return null; // No crear el consumidor si no hay consumidor configurado
            }
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = options.BootstrapServers,
                SaslUsername = options.ConsumerUsername,
                SaslPassword = options.ConsumerPassword,
                SaslMechanism = SaslMechanism.ScramSha512,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                GroupId = options.GroupId,
            };
            var schemaRegistryConfig = new SchemaRegistryConfig { Url = options.SchemaRegistryUrl };
            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            return new ConsumerBuilder<string, GenericRecord>(consumerConfig)
                .SetValueDeserializer(new AvroDeserializer<GenericRecord>(schemaRegistry).AsSyncOverAsync())
                .Build();
        });

        // Configurar OpenTelemetry
        builder.Services.AddOpenTelemetry()
           .WithTracing(tracing =>
           {
               tracing.AddSource(KafkaTelemetry.ActivitySourceName);
           });

        // Agregar servicios de Kafka
        builder.Services.AddSingleton<KafkaTelemetry>();
        builder.Services.AddSingleton<IEventBus, KafkaEventBus>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<KafkaEventBus>>();
            var serviceProvider = sp.GetRequiredService<IServiceProvider>();
            var options = sp.GetRequiredService<IOptions<EventBusOptions>>();
            var subscriptionOptions = sp.GetRequiredService<IOptions<EventBusSubscriptionInfo>>();
            var kafkaTelemetry = sp.GetRequiredService<KafkaTelemetry>();
            var producer = sp.GetService<IProducer<string, GenericRecord>>();
            var consumer = sp.GetService<IConsumer<string, GenericRecord>>();

            return new KafkaEventBus(logger, serviceProvider, options, subscriptionOptions, kafkaTelemetry, producer, consumer);
        });
        builder.Services.AddSingleton<IHostedService>(sp => (KafkaEventBus)sp.GetRequiredService<IEventBus>());

        return new EventBusBuilder(builder.Services);
    }

    private class EventBusBuilder : IEventBusBuilder
    {
        public EventBusBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
