namespace UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;

public class CreatePeripheralRequest
{
    public Guid? Id { get; set; }
    public string Code { get; set; }
    public string SerialNumber { get; set; }
}
