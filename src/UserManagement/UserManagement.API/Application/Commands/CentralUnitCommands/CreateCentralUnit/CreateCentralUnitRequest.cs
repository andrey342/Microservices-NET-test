namespace UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;

public class CreateCentralUnitRequest
{
    public Guid? Id { get; set; }
    public string Code { get; set; }
    public string SerialNumber { get; set; }
    public string Phone { get; set; }
}
