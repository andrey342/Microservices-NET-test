namespace UserManagement.API.Application.IntegrationEvents.Events;

public record CentralUnitUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public string SerialNumber { get; init; }
    public string Phone { get; init; }


    public CentralUnitUpdatedIntegrationEvent(Guid id, string code, string serialNumber, string phone)
    {
        Id = id;
        Code = code;
        SerialNumber = serialNumber;
        Phone = phone;
    }
}

