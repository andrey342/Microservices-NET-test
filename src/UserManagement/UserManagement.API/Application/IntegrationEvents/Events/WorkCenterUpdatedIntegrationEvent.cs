namespace UserManagement.API.Application.IntegrationEvents.Events;

public record WorkCenterUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; }
    public string Name { get; }

    public WorkCenterUpdatedIntegrationEvent(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
