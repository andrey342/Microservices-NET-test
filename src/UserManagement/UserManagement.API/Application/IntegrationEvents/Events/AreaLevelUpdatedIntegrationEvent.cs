namespace UserManagement.API.Application.IntegrationEvents.Events;

public record AreaLevelUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Level { get; init; }
    public Guid WorkCenterId { get; init; }

    public AreaLevelUpdatedIntegrationEvent() { }

    public AreaLevelUpdatedIntegrationEvent(AreaLevelUpdatedIntegrationEvent areaLevelUpdatedIntegrationEvent)
        : base(areaLevelUpdatedIntegrationEvent)
    {
        Id = areaLevelUpdatedIntegrationEvent.Id;
        Name = areaLevelUpdatedIntegrationEvent.Name;
        Level = areaLevelUpdatedIntegrationEvent.Level;
        WorkCenterId = areaLevelUpdatedIntegrationEvent.WorkCenterId;
    }
}