using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;

public class CreateMedicalInformationCommand : IRequest<Result<MedicalInformationViewModel>>
{
    public CreateMedicalInformationRequest CreateMedicalInformationRequest { get; private set; }

    public CreateMedicalInformationCommand(CreateMedicalInformationRequest createMedicalInformationRquest)
    {
        CreateMedicalInformationRequest = createMedicalInformationRquest;
    }
}