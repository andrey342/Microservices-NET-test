namespace UserManagement.API.Application.Commands.CentralUnitCommands.DeleteCentralUnit;

public class DeleteCentralUnitCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteCentralUnitCommand(Guid id)
    {
        Id = id;
    }
}