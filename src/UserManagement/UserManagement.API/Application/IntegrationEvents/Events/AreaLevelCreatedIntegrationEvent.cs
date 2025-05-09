namespace UserManagement.API.Application.IntegrationEvents.Events;

public record AreaLevelCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Level { get; init; }
    public Guid WorkCenterId { get; init; }

    public AreaLevelCreatedIntegrationEvent() { }

    public AreaLevelCreatedIntegrationEvent(AreaLevelCreatedIntegrationEvent areaLevelCreatedIntegrationEvent)
        : base(areaLevelCreatedIntegrationEvent)
    {
        Id = areaLevelCreatedIntegrationEvent.Id;
        Name = areaLevelCreatedIntegrationEvent.Name;
        Level = areaLevelCreatedIntegrationEvent.Level;
        WorkCenterId = areaLevelCreatedIntegrationEvent.WorkCenterId;
    }
}