namespace UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;

/*No hereda de CreatePeripheralRequest para no permitir modificar el serialnumber*/
public class UpdatePeripheralRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string SerialNumber { get; set; }
}