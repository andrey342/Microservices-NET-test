namespace UserManagement.API.Application.IntegrationEvents.Events;

public record UserTypeUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid WorkCenterId { get; init; }

    public UserTypeUpdatedIntegrationEvent() { }

    public UserTypeUpdatedIntegrationEvent(UserTypeUpdatedIntegrationEvent userTypeUpdatedIntegrationEvent)
        : base(userTypeUpdatedIntegrationEvent)
    {
        Id = userTypeUpdatedIntegrationEvent.Id;
        Name = userTypeUpdatedIntegrationEvent.Name;
        WorkCenterId = userTypeUpdatedIntegrationEvent.WorkCenterId;
    }
}
