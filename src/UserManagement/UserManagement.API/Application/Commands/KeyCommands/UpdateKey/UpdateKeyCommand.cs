using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.KeyCommands.UpdateKey;

public class UpdateKeyCommand : IRequest<Result<KeyViewModel>>
{
    public UpdateKeyRequest Request { get; set; }

    public UpdateKeyCommand(UpdateKeyRequest request)
    {
        Request = request;
    }
}