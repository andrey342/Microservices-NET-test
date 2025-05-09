namespace UserManagement.API.Application.IntegrationEvents.Events;
    public record AreaUpdatedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid AreaLevelId { get; init; }
        public Guid? ParentId { get; init; } = null;

        public AreaUpdatedIntegrationEvent() { }

        public AreaUpdatedIntegrationEvent(AreaUpdatedIntegrationEvent areaUpdatedIntegrationEvent)
            : base(areaUpdatedIntegrationEvent)
        {
            Id = areaUpdatedIntegrationEvent.Id;
            Name = areaUpdatedIntegrationEvent.Name;
            AreaLevelId = areaUpdatedIntegrationEvent.AreaLevelId;
            ParentId = areaUpdatedIntegrationEvent.ParentId;
        }
    }