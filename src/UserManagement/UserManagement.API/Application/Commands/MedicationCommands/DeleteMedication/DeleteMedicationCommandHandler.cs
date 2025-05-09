using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicationCommands.DeleteMedication;

public class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    public DeleteMedicationCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<Unit>> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
    {
        var medication = await _userRepository.GetMedicationById(request.Id);

        var medicalInformation = await _userRepository.GetMedicalInformationById(medication.MedicalInformationId);
        if (medicalInformation == null)
        {
            return Result<Unit>.FailureResult("Medical Information not found.");
        }

        medicalInformation.RemoveMedication(medication);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Medical Condition removed successfully.");
    }
}