namespace UserManagement.API.Application.Commands.MedicationCommands.UpdateMedication;

public class UpdateMedicationRequest
{
    public Guid Id { get; set; }
    public string? Dosage { get; set; }
}
