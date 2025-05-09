namespace UserManagement.API.Application.IntegrationEvents.Events;

public record AreaDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public AreaDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}