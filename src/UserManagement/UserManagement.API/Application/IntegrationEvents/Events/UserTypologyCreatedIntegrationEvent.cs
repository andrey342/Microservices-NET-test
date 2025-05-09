namespace UserManagement.API.Application.IntegrationEvents.Events;

public record UserTypologyCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid WorkCenterId { get; init; }

    public UserTypologyCreatedIntegrationEvent() { }

    public UserTypologyCreatedIntegrationEvent(UserTypologyCreatedIntegrationEvent userTypologyCreatedIntegrationEvent)
        : base(userTypologyCreatedIntegrationEvent)
    {
        Id = userTypologyCreatedIntegrationEvent.Id;
        Name = userTypologyCreatedIntegrationEvent.Name;
        WorkCenterId = userTypologyCreatedIntegrationEvent.WorkCenterId;
    }
}
