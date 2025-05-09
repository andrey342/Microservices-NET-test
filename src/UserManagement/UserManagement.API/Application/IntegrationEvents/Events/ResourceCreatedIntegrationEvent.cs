using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.IntegrationEvents.Events;

public record ResourceCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public PhoneNumbers PhoneNumbers { get; init; }
    public Guid WorkCenterId { get; init; }

    public ResourceCreatedIntegrationEvent() { }

    public ResourceCreatedIntegrationEvent(ResourceCreatedIntegrationEvent resourceCreatedIntegrationEvent)
        : base(resourceCreatedIntegrationEvent)
    {
        Id = resourceCreatedIntegrationEvent.Id;
        Name = resourceCreatedIntegrationEvent.Name;
        WorkCenterId = resourceCreatedIntegrationEvent.WorkCenterId;
        PhoneNumbers = resourceCreatedIntegrationEvent.PhoneNumbers;
    }
}