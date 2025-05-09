namespace UserManagement.API.Application.Commands.UserTypeCommands.DeleteUserType;

public class DeleteUserTypeCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteUserTypeCommand(Guid id)
    {
        Id = id;
    }
}