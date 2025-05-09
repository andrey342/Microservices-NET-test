using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;
public class CreateResidenceCommand : IRequest<Result<ResidenceViewModel>>
{
    public CreateResidenceRequest AddResidenceToServiceContractRequest { get; private set; }

    public CreateResidenceCommand(CreateResidenceRequest addResidenceToServiceContractRequest)
    {
        AddResidenceToServiceContractRequest = addResidenceToServiceContractRequest;
    }
}