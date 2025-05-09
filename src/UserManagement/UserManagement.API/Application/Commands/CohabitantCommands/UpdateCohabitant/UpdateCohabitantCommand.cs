using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.CohabitantCommands.UpdateCohabitant;

public class UpdateCohabitantCommand : IRequest<Result<CohabitantViewModel>>
{
    public UpdateCohabitantRequest Request { get; set; }

    public UpdateCohabitantCommand(UpdateCohabitantRequest request)
    {
        Request = request;
    }
}