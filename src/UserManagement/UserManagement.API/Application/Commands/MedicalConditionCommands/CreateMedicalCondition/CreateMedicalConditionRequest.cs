namespace UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;

public class CreateMedicalConditionRequest
{
    public Guid MedicalInformationId { get; set; }
    public Guid DiseaseId { get; set; }
    public Guid StatusId { get; set; }
    public DateTime DiagnosedDate { get; set; }
}