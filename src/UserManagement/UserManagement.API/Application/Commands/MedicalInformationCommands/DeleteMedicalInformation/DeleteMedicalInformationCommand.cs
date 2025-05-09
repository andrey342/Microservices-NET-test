namespace UserManagement.API.Application.Commands.MedicalInformationCommands.DeleteMedicalInformation;

public class DeleteMedicalInformationCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }


    public DeleteMedicalInformationCommand(Guid id)
    {
        Id = id;
    }
}