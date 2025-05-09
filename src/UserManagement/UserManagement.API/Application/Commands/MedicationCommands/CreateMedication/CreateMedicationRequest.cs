namespace UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;

public class CreateMedicationRequest
{
    public Guid MedicalInformationId { get; set; }
    public Guid MedicineId { get; set; }
    public string Dosage { get; set; }
}
