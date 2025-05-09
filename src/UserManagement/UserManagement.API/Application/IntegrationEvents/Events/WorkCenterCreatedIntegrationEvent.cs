namespace UserManagement.API.Application.IntegrationEvents.Events;

public record WorkCenterCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; }
    public string Name { get; }

    public WorkCenterCreatedIntegrationEvent(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
