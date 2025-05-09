namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddPeripheral;

public class AddPeripheralCommand : IRequest<Result<Guid>>
{
    public AddPeripheralRequest PeripheralRequest { get; private set; }

    public AddPeripheralCommand(AddPeripheralRequest peripheralRequest)
    {
        PeripheralRequest = peripheralRequest;
    }
}