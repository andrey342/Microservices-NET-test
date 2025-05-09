using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract;

public class CreateServiceContractCommand : IRequest<Result<CreatedContractViewModel>>
{
    public CreateServiceContractRequest ServiceContractRequest { get; private set; }

    public CreateServiceContractCommand(CreateServiceContractRequest serviceContractRequest)
    {
        ServiceContractRequest = serviceContractRequest;
    }
}