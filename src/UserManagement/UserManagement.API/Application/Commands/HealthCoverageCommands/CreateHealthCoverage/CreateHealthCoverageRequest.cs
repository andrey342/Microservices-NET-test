namespace UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;

public class CreateHealthCoverageRequest
{
    public Guid MedicalInformationId { get; set; }
    public string Provider { get; set; }
    public string PolicyNumber { get; set; }
    public string CoverageType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
