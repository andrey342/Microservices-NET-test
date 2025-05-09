using UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;

namespace UserManagement.API.Application.Commands.CohabitantCommands.UpdateCohabitant;

public class UpdateCohabitantRequest : CreateCohabitantRequest
{
    public Guid Id { get; set; }
}