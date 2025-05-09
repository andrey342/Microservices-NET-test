using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;

public class UpdateMedicalConditionCommand : IRequest<Result<MedicalConditionViewModel>>
{
    public UpdateMedicalConditionRequest UpdateMedicalConditionRequest { get; private set; }

    public UpdateMedicalConditionCommand(UpdateMedicalConditionRequest updateMedicalConditionRequest)
    {
        UpdateMedicalConditionRequest = updateMedicalConditionRequest;
    }
}