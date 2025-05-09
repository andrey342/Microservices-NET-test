using UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddPeripheral;

public class AddPeripheralRequest
{
    public Guid? PeripheralId { get; set; } // Puede ser null si el usuario no existe
    public CreatePeripheralRequest? Peripheral { get; set; } // Información del usuario si necesita ser creado
    public Guid ServiceContractCentralUnitId { get; set; }
}
