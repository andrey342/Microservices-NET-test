namespace UserManagement.API.Application.IntegrationEvents.Events;

public record ResourceDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public ResourceDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}