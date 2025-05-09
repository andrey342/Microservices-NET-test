namespace UserManagement.API.Application.IntegrationEvents.Events;

public record UserTypeCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid WorkCenterId { get; init; }

    public UserTypeCreatedIntegrationEvent() { }

    public UserTypeCreatedIntegrationEvent(UserTypeCreatedIntegrationEvent userTypeCreatedIntegrationEvent)
        : base(userTypeCreatedIntegrationEvent)
    {
        Id = userTypeCreatedIntegrationEvent.Id;
        Name = userTypeCreatedIntegrationEvent.Name;
        WorkCenterId = userTypeCreatedIntegrationEvent.WorkCenterId;
    }
}
