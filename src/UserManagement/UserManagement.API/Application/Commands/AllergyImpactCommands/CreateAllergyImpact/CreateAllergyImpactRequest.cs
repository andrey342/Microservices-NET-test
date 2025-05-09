namespace UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;

public class CreateAllergyImpactRequest
{
    public Guid MedicalInformationId { get; set; }
    public Guid AllergyId { get; set; }
    public Guid SeverityId { get; set; }
    public string? Reaction { get; set; }
}
