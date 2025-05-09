using UserManagement.API.Application.Commands.IdentificationCommands.CreateIdentification;
using UserManagement.API.Application.Commands.PreferredProfessionalCommands.CreatePreferredProfessional;

namespace UserManagement.API.Application.Commands.UserCommands.CreateUser;

public class CreateUserRequest
{
    public string Name { get; set; } = null!;
    public string Surname1 { get; set; } = null!;
    public string? Surname2 { get; set; } = null;
    public string? Appellative { get; set; } = null;
    public Guid SexId { get; set; }
    public Guid? IdentificationId { get; set; } = null;
    public DateTime Birthdate { get; set; }
    public bool? CongratulateOnBirthDate { get; set; } = null;
    public Guid CivilStatusId { get; set; }
    public Guid? LanguageId { get; set; } = null;
    public Guid? EducationId { get; set; } = null;
    public string? Mobile { get; set; } = null;
    public string? Phone { get; set; } = null;
    public string? Email { get; set; } = null;
    public Guid? DependencyId { get; set; } = null;
    public string? CallTime { get; set; } = null;
    public string? Observation { get; set; } = null;
    public Guid? MedicalInformationId { get; set; } = null;
    public CreateIdentificationRequest Identification { get; set; } = null!;
    public CreatePreferredProfessionalRequest? PreferredProfessional { get; set; } = null;
}
