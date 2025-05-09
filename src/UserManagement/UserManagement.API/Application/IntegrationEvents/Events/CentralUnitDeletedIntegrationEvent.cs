namespace UserManagement.API.Application.IntegrationEvents.Events;

public record CentralUnitDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public CentralUnitDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}

