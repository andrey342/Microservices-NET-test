using UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddCentralUnit;

public class AddCentralUnitRequest
{
    public Guid? CentralUnitId { get; set; } // Puede ser null si el usuario no existe
    public CreateCentralUnitRequest? CentralUnit { get; set; } // Información del usuario si necesita ser creado
    public Guid ServiceContractId { get; set; }
}
