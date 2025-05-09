namespace UserManagement.API.Application.Commands.KeyCommands.CreateKey;

public class CreateKeyRequest
{
    public Guid ResidenceId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Keys { get; set; }
}
