using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.UpdateMedicalInformation;

public class UpdateMedicalInformationCommand : IRequest<Result<MedicalInformationViewModel>>
{
    public UpdateMedicalInformationRequest UpdateMedicalInformationRequest { get; private set; }

    public UpdateMedicalInformationCommand(UpdateMedicalInformationRequest updateMedicalInformationRequest)
    {
        UpdateMedicalInformationRequest = updateMedicalInformationRequest;
    }
}