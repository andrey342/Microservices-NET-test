namespace UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;

public class CreateMedicalInformationRequest
{
    public Guid UserId { get; set; }
    public Guid? DependencyDegreeId { get; set; }
    public int? BarthelIndex { get; set; }
    public int? LawtonIndex { get; set; }
    public int? PhysicalScaleBSN { get; set; }
    public int? PhysicalScaleBVD { get; set; }
    public int? PsychologicalScaleBSN { get; set; }
    public int? PsychologicalScaleBVD { get; set; }
    public int? SocialScaleBSN { get; set; }
    public int? SocialScaleBVD { get; set; }
    public string? Observation { get; set; }
}