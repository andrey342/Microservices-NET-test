using IntegrationEventLog.Services;

namespace UserManagement.API.Application.Services;
public class RetryFailedIntegrationEventService : IHostedService, IDisposable
{
    private readonly ILogger<RetryFailedIntegrationEventService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;

    public RetryFailedIntegrationEventService(ILogger<RetryFailedIntegrationEventService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RetryFailedEventsHostedService started.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        _logger.LogInformation("RetryFailedEventsHostedService is working.");

        using (var scope = _serviceProvider.CreateScope())
        {
            var eventLogService = scope.ServiceProvider.GetRequiredService<IIntegrationEventLogService>();
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

            var failedLogEvents = await eventLogService.RetrieveFailedEventLogsAsync();

            foreach (var eventLog in failedLogEvents)
            {
                if (await eventLogService.TryLockEventAsync(eventLog.EventId))
                {
                    _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", eventLog.EventId, eventLog.IntegrationEvent);
                    try
                    {
                        await eventLogService.MarkEventAsInProgressAsync(eventLog.EventId);
                        await eventBus.PublishAsync(eventLog.IntegrationEvent);
                        await eventLogService.MarkEventAsPublishedAsync(eventLog.EventId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", eventLog.EventId);

                        await eventLogService.MarkEventAsFailedAsync(eventLog.EventId);
                    }
                    finally
                    {
                        await eventLogService.UnlockEventAsync(eventLog.EventId);
                    }
                }
            }
        }
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RetryFailedEventsHostedService stopped.");

        _timer.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}
