namespace UserManagement.API.Application.Commands.AreaCommands.DeleteArea;

public class DeleteAreaCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteAreaCommand(Guid id)
    {
        Id = id;
    }
}
