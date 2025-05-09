using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.MedicationCommands.UpdateMedication;

public class UpdateMedicationCommand : IRequest<Result<MedicationViewModel>>
{
    public UpdateMedicationRequest UpdateMedicationRequest { get; private set; }

    public UpdateMedicationCommand(UpdateMedicationRequest updateMedicationRequest)
    {
        UpdateMedicationRequest = updateMedicationRequest;
    }
}