using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContract;

public class UpdateServiceContractCommand : IRequest<Result<BasicServiceContractViewModel>>
{
    public UpdateServiceContractRequest ServiceContractRequest { get; set; }

    public UpdateServiceContractCommand(UpdateServiceContractRequest serviceContractRequest)
    {
        ServiceContractRequest = serviceContractRequest;
    }
}