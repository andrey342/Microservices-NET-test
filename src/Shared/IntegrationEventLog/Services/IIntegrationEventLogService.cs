namespace IntegrationEventLog.Services;

public interface IIntegrationEventLogService
{
    Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
    Task<IntegrationEventLogEntry> RetrieveEventLogPendingToPublishByEventIdAsync(Guid EventId);
    Task<IEnumerable<IntegrationEventLogEntry>> RetrieveFailedEventLogsAsync();
    Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
    Task MarkEventAsPublishedAsync(Guid eventId);
    Task MarkEventAsInProgressAsync(Guid eventId);
    Task MarkEventAsFailedAsync(Guid eventId);
    Task<bool> TryLockEventAsync(Guid eventId);
    Task UnlockEventAsync(Guid eventId);
}
