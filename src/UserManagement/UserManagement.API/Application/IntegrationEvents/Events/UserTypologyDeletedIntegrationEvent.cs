namespace UserManagement.API.Application.IntegrationEvents.Events;

public record UserTypologyDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }

    public UserTypologyDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}