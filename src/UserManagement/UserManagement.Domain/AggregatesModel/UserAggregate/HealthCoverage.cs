using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class HealthCoverage : Entity
{
    public Guid MedicalInformationId { get; private set; }
    public MedicalInformation MedicalInformation { get; private set; }
    public string Provider { get; private set; }
    public string PolicyNumber { get; private set; }
    public string CoverageType { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private HealthCoverage() { }

    public HealthCoverage(HealthCoverage healthCoverage)
    {
        this.CopyPropertiesTo(healthCoverage);
    }
    public void UpdateMedicalInformationId(Guid medicalInformationId)
    {
        MedicalInformationId = medicalInformationId;
    }

    public void Update(HealthCoverage healthCoverage)
    {
        this.CopyPropertiesTo(healthCoverage);
    }
}
