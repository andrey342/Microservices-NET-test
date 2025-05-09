namespace UserManagement.API.Application.Commands.UserTypologyCommands.DeleteUserTypology;

public class DeleteUserTypologyCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteUserTypologyCommand(Guid id)
    {
        Id = id;
    }
}