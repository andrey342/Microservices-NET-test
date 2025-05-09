using UserManagement.API.Application.Common.Models;

namespace UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;
public class CreateCohabitantCommand : IRequest<Result<Guid>>
{
    public CreateCohabitantRequest Request { get; }

    public CreateCohabitantCommand(CreateCohabitantRequest request)
    {
        Request = request;
    }
}