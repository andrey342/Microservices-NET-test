using UserManagement.API.Application.Queries.PeripheralQueries;

namespace UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;

public class UpdatePeripheralCommand : IRequest<Result<PeripheralViewModel>>
{
    public UpdatePeripheralRequest PeripheralRequest { get; set; }

    public UpdatePeripheralCommand(UpdatePeripheralRequest peripheralRequest)
    {
        PeripheralRequest = peripheralRequest;
    }
}