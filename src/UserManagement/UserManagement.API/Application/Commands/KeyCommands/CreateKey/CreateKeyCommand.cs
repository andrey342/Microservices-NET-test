namespace UserManagement.API.Application.Commands.KeyCommands.CreateKey;

public class CreateKeyCommand : IRequest<Result<Guid>>
{
    public CreateKeyRequest Request { get; }

    public CreateKeyCommand(CreateKeyRequest request)
    {
        Request = request;
    }
}
