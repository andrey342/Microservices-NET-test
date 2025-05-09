namespace UserManagement.API.Application.Commands.PreferredProfessionalCommands.CreatePreferredProfessional;

public class CreatePreferredProfessionalRequest
{
    public Guid ProfessionalId { get; set; }
    public string Name { get; set; }
    public string Surname1 { get; set; }
    public string? Surname2 { get; set; }
}
