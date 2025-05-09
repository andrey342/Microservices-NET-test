using UserManagement.API.Application.Commands.KeyCommands.CreateKey;

namespace UserManagement.API.Application.Commands.KeyCommands.UpdateKey;

public class UpdateKeyRequest : CreateKeyRequest
{
    public Guid Id { get; set; }
    public Guid CurrentStatusId { get; set; }
}
