using UserManagement.API.Application.Queries.CentralUnitQueries;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;

public class UpdateCentralUnitCommand : IRequest<Result<CentralUnitViewModel>>
{
    public UpdateCentralUnitRequest CentralUnitRequest { get; set; }

    public UpdateCentralUnitCommand(UpdateCentralUnitRequest centralUnitRequest)
    {
        CentralUnitRequest = centralUnitRequest;
    }
}