using UserManagement.API.Application.Common.Models;

namespace UserManagement.API.Application.Commands.PreferredProfessionalCommands.UpdatePreferredProfessional;

public class UpdatePreferredProfessionalCommand : IRequest<Result<Guid>>
{
    public Guid? Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public string Name { get; set; }
    public string Surname1 { get; set; }
    public string? Surname2 { get; set; }

    public UpdatePreferredProfessionalCommand(Guid id, string name, string surname1, string surname2)
    {
        ProfessionalId = id;
        Name = name;
        Surname1 = surname1;
        Surname2 = surname2;
    }
}