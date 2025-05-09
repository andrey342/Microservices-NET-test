namespace UserManagement.API.Application.Commands.MedicalConditionCommands.RemoveMedicalCondition;

public class DeleteMedicalConditionCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public DeleteMedicalConditionCommand(Guid id)
    {
        Id = id;
    }
}