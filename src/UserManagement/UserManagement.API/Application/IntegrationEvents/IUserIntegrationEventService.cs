namespace UserManagement.API.Application.IntegrationEvents;

public interface IUserIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}
