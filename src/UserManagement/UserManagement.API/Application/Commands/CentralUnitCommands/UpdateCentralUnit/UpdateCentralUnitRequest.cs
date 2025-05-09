namespace UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;

/*No hereda de CreateCentralUnitRequest para no permitir modificar el serialnumber*/
public class UpdateCentralUnitRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string SerialNumber { get; set; }
    public string Phone { get; set; }
}