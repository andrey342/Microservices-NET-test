namespace UserManagement.API.Application.Commands.MedicationCommands.DeleteMedication;

public class DeleteMedicationCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public DeleteMedicationCommand(Guid id)
    {
        Id = id;
    }
}