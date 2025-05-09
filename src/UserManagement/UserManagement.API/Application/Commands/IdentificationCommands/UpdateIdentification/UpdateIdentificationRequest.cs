using UserManagement.API.Application.Commands.IdentificationCommands.CreateIdentification;

namespace UserManagement.API.Application.Commands.IdentificationCommands.UpdateIdentification;
public class UpdateIdentificationRequest : CreateIdentificationRequest
{
    public Guid Id { get; set; }
}
