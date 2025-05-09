namespace UserManagement.API.Application.Commands.AreaLevelCommands.DeleteAreaLevel;

public class DeleteAreaLevelCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteAreaLevelCommand(Guid id)
    {
        Id = id;
    }
}
