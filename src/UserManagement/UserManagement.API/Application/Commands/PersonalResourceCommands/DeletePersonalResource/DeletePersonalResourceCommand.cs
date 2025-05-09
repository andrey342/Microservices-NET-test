namespace UserManagement.API.Application.Commands.PersonalResourceCommands.DeletePersonalResource;

public class DeletePersonalResourceCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; }

    public DeletePersonalResourceCommand(Guid id)
    {
        Id = id;
    }
}
