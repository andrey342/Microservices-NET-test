namespace UserManagement.API.Application.IntegrationEvents.Events;

public record PeripheralDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public PeripheralDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}
