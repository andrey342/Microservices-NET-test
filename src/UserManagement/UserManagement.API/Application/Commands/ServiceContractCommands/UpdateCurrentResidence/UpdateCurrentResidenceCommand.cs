using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateCurrentResidence;

public class UpdateCurrentResidenceCommand: IRequest<Result<FullServiceContractViewModel>>
{
    public UpdateCurrentResidenceRequest ActiveResidenceRequest { get; private set; }

    public UpdateCurrentResidenceCommand(UpdateCurrentResidenceRequest activeResidenceRequest)
    {
        ActiveResidenceRequest = activeResidenceRequest;
    }
}

