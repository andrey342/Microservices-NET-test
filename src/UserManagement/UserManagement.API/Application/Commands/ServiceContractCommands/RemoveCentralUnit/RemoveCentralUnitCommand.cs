namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemoveCentralUnit;

public class RemoveCentralUnitCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public RemoveCentralUnitCommand(Guid id)
    {
        Id = id;
    }
}