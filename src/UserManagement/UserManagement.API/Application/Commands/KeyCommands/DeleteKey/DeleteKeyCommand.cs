namespace UserManagement.API.Application.Commands.KeyCommands.DeleteKey;

public class DeleteKeyCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public DeleteKeyCommand(Guid id)
    {
        Id = id;
    }
}
