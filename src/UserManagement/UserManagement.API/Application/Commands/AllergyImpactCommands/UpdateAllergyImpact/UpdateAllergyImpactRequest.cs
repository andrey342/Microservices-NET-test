namespace UserManagement.API.Application.Commands.AllergyImpactCommands.UpdateAllergyImpact;

public class UpdateAllergyImpactRequest
{
    public Guid Id { get; set; }
    public Guid SeverityId { get; set; }
    public string Reaction { get; set; }
}
