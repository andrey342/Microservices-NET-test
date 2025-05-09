using UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;

namespace UserManagement.API.Application.Commands.ResidenceCommands.UpdateResidence;

public class UpdateResidenceRequest : CreateResidenceRequest
{
    public Guid Id { get; set; }

}
