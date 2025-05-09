using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class MedicalCondition : Entity
{
    public Guid MedicalInformationId { get; private set; }
    public MedicalInformation MedicalInformation { get; private set; }
    public Guid DiseaseId { get; private set; }
    public Disease Disease { get; private set; }
    public Guid StatusId { get; private set; }
    public MedicalConditionStatus Status { get; private set; }
    public DateTime DiagnosedDate { get; private set; }

    private MedicalCondition() { }

    public MedicalCondition(MedicalCondition medicalCondition)
    {
        this.CopyPropertiesTo(medicalCondition);
    }
    public void UpdateMedicalInformationId(Guid medicalInformationId)
    {
        MedicalInformationId = medicalInformationId;
    }

    public void Update(MedicalCondition medicalCondition)
    {
        this.CopyPropertiesTo(medicalCondition);
    }
}
