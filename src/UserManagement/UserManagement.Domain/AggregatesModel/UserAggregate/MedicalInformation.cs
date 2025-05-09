using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class MedicalInformation : Entity
{
    public Guid? DependencyDegreeId { get; private set; }
    public DependencyDegree? DependencyDegree { get; private set; }
    public int? BarthelIndex { get; private set; }
    public int? LawtonIndex { get; private set; }
    public int? PhysicalScaleBSN { get; private set; }
    public int? PhysicalScaleBVD { get; private set; }
    public int? PsychologicalScaleBSN { get; private set; }
    public int? PsychologicalScaleBVD { get; private set; }
    public int? SocialScaleBSN { get; private set; }
    public int? SocialScaleBVD { get; private set; }
    public string? Observation { get; private set; }

    private List<MedicalCondition> _medicalConditions = new();
    public IReadOnlyCollection<MedicalCondition> MedicalConditions => _medicalConditions.AsReadOnly();

    private List<Medication> _medications = new();
    public IReadOnlyCollection<Medication> Medications => _medications.AsReadOnly();

    private List<AllergyImpact> _allergyImpacts = new();
    public IReadOnlyCollection<AllergyImpact> AllergyImpacts => _allergyImpacts.AsReadOnly();

    private List<HealthCoverage> _healthCoverages = new();
    public IReadOnlyCollection<HealthCoverage> HealthCoverages => _healthCoverages.AsReadOnly();

    private MedicalInformation() { }

    public MedicalInformation(MedicalInformation medicalInformation)
    {
        this.CopyPropertiesTo(medicalInformation);
    }

    public void AddMedicalCondition(MedicalCondition condition)
    {
        _medicalConditions.Add(condition);
    }

    public void RemoveMedicalCondition(MedicalCondition condition)
    {
        _medicalConditions.Remove(condition);
    }

    public void AddHealthCoverage(HealthCoverage healthCoverage)
    {
        healthCoverage.UpdateMedicalInformationId(Id);
        _healthCoverages.Add(healthCoverage);
    }

    public void RemoveHealthCoverage(HealthCoverage healthCoverage)
    {
        _healthCoverages.Remove(healthCoverage);
    }

    public void AddMedication(Medication medication)
    {
        medication.UpdateMedicalInformationId(Id);
        _medications.Add(medication);
    }

    public void RemoveMedication(Medication medication)
    {
        _medications.Remove(medication);
    }

    public void AddAllergyImpact(AllergyImpact allergyImpact)
    {
        _allergyImpacts.Add(allergyImpact);
    }

    public void RemoveAllergyImpact(AllergyImpact allergyImpact)
    {
        _allergyImpacts.Remove(allergyImpact);
    }

    public void Update(MedicalInformation updatedInfo)
    {
        this.CopyPropertiesTo(updatedInfo);
    }
}
