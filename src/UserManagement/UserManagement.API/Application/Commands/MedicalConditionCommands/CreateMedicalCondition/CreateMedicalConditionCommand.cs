namespace UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;

public class CreateMedicalConditionCommand : IRequest<Result<Guid>>
{
    public CreateMedicalConditionRequest AddMedicalConditionRequest { get; private set; }

    public CreateMedicalConditionCommand(CreateMedicalConditionRequest addMedicalConditionRequest)
    {
        AddMedicalConditionRequest = addMedicalConditionRequest;
    }
}