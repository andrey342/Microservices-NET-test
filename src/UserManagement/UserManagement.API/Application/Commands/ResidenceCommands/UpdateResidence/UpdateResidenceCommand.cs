using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.ResidenceCommands.UpdateResidence;

public class UpdateResidenceCommand : IRequest<Result<ResidenceViewModel>>
{
    public UpdateResidenceRequest UpdateResidenceInServiceContractRequest { get; private set; }

    public UpdateResidenceCommand(UpdateResidenceRequest updateResidenceInServiceContractRequest)
    {
        UpdateResidenceInServiceContractRequest = updateResidenceInServiceContractRequest;
    }
}
