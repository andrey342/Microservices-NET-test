namespace UserManagement.API.Application.IntegrationEvents.Events;

public record AreaCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid AreaLevelId { get; init; }
    public Guid? ParentId { get; init; }
    public AreaCreatedIntegrationEvent() { }

    public AreaCreatedIntegrationEvent(AreaCreatedIntegrationEvent areaCreatedIntegrationEvent)
        : base(areaCreatedIntegrationEvent)
    {
        Id = areaCreatedIntegrationEvent.Id;
        Name = areaCreatedIntegrationEvent.Name;
        AreaLevelId = areaCreatedIntegrationEvent.AreaLevelId;
    }
}