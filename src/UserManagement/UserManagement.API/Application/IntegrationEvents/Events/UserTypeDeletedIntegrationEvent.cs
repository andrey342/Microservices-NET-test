namespace UserManagement.API.Application.IntegrationEvents.Events;

public record UserTypeDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public UserTypeDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}