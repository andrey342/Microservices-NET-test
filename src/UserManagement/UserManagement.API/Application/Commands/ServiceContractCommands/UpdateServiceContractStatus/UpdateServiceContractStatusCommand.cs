using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContractStatus;

public class UpdateServiceContractStatusCommand
: IRequest<Result<FullServiceContractViewModel>>
{
    public UpdateServiceContractStatusRequest UpdateServiceContractStatusRequest { get; private set; }

    public UpdateServiceContractStatusCommand(UpdateServiceContractStatusRequest updateServiceContractStatusRequest)
    {
        UpdateServiceContractStatusRequest = updateServiceContractStatusRequest;
    }
}
