namespace UserManagement.API.Application.Commands.ResourceCommands.DeleteResource;

public class DeleteResourceCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteResourceCommand(Guid id)
    {
        Id = id;
    }
}