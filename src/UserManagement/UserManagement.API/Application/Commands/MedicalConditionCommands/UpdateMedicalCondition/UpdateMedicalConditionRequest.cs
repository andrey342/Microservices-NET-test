namespace UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;

public class UpdateMedicalConditionRequest
{
    public Guid Id { get; set; }
    public Guid StatusId { get; set; }
    public DateTime DiagnosedDate { get; set; }
}
