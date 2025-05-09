namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemovePeripheral;

public class RemovePeripheralCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public RemovePeripheralCommand(Guid id)
    {
        Id = id;
    }
}