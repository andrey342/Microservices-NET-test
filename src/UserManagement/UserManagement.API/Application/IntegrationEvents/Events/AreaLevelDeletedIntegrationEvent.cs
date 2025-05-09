namespace UserManagement.API.Application.IntegrationEvents.Events;

public record AreaLevelDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public AreaLevelDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}