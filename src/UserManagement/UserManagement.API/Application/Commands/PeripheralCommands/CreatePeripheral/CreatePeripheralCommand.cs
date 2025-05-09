namespace UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;

public class CreatePeripheralCommand : IRequest<Result<Guid>>
{
    public CreatePeripheralRequest PeripheralRequest { get; private set; }

    public CreatePeripheralCommand(CreatePeripheralRequest peripheralRequest)
    {
        PeripheralRequest = peripheralRequest;
    }
}