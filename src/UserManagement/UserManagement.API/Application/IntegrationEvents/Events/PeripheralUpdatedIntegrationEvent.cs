namespace UserManagement.API.Application.IntegrationEvents.Events;

public record PeripheralUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public string SerialNumber { get; init; }


    public PeripheralUpdatedIntegrationEvent(Guid id, string code, string serialNumber)
    {
        Id = id;
        Code = code;
        SerialNumber = serialNumber;
    }
}