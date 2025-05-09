namespace UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;

public class CreateMedicationCommand : IRequest<Result<Guid>>
{
    public CreateMedicationRequest CreateMedicationRequest { get; private set; }

    public CreateMedicationCommand(CreateMedicationRequest createMedicationRequest)
    {
        CreateMedicationRequest = createMedicationRequest;
    }
}