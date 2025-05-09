using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.IntegrationEvents.Events;

public record ResourceUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public PhoneNumbers PhoneNumbers { get; init; }
    public Guid WorkCenterId { get; init; }

    public ResourceUpdatedIntegrationEvent() { }

    public ResourceUpdatedIntegrationEvent(ResourceUpdatedIntegrationEvent resourceUpdatedIntegrationEvent)
        : base(resourceUpdatedIntegrationEvent)
    {
        Id = resourceUpdatedIntegrationEvent.Id;
        Name = resourceUpdatedIntegrationEvent.Name;
        WorkCenterId = resourceUpdatedIntegrationEvent.WorkCenterId;
        PhoneNumbers = resourceUpdatedIntegrationEvent.PhoneNumbers;
    }
}