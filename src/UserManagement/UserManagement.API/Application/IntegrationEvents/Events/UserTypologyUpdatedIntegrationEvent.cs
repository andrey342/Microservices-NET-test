namespace UserManagement.API.Application.IntegrationEvents.Events;

public record UserTypologyUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid WorkCenterId { get; init; }

    public UserTypologyUpdatedIntegrationEvent() { }

    public UserTypologyUpdatedIntegrationEvent(UserTypologyUpdatedIntegrationEvent userTypologyUpdatedIntegrationEvent)
        : base(userTypologyUpdatedIntegrationEvent)
    {
        Id = userTypologyUpdatedIntegrationEvent.Id;
        Name = userTypologyUpdatedIntegrationEvent.Name;
        WorkCenterId = userTypologyUpdatedIntegrationEvent.WorkCenterId;
    }
}