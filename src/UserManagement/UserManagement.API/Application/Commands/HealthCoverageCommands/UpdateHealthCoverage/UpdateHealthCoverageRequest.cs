namespace UserManagement.API.Application.Commands.HealthCoverageCommands.UpdateHealthCoverage;

public class UpdateHealthCoverageRequest
{
    public Guid Id { get; set; }
    public string PolicyNumber { get; set; }
    public string CoverageType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
