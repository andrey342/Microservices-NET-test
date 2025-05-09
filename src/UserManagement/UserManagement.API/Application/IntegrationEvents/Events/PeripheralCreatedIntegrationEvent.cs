namespace UserManagement.API.Application.IntegrationEvents.Events;

public record PeripheralCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public string SerialNumber { get; init; }


    public PeripheralCreatedIntegrationEvent(Guid id, string code, string serialNumber)
    {
        Id = id;
        Code = code;
        SerialNumber = serialNumber;
    }
}