namespace UserManagement.API.Application.Commands.PeripheralCommands.DeletePeripheral;

public class DeletePeripheralCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeletePeripheralCommand(Guid id)
    {
        Id = id;
    }
}
