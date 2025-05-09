using System.Data;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class Medication : Entity
{
    public Guid MedicalInformationId { get; private set; }
    public MedicalInformation MedicalInformation { get; private set; }
    public string? Dosage { get; private set; }
    public string? Recurrence { get; private set; } // Ejemplo: "Every 8 hours"
    public Guid MedicineId { get; private set; }
    public Medicine Medicine { get; private set; }

    private Medication() { }

    public Medication(Medication medication)
    {
        this.CopyPropertiesTo(medication);
    }

    public void Update(Medication medication)
    {
        this.CopyPropertiesTo(medication);
    }
    public void UpdateMedicalInformationId(Guid medicalInformationId)
    {
        MedicalInformationId = medicalInformationId;
    }
    public void UpdateDosage(string dosage, string? recurrence)
    {
        Dosage = dosage;
        Recurrence = recurrence;
    }
}
