using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.RemoveMedicalCondition;

public class DeleteMedicalConditionCommandHandler : IRequestHandler<DeleteMedicalConditionCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    public DeleteMedicalConditionCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<Unit>> Handle(DeleteMedicalConditionCommand request, CancellationToken cancellationToken)
    {
        var medicalCondition = await _userRepository.GetMedicalConditionById(request.Id);

        var medicalInformation = await _userRepository.GetMedicalInformationById(medicalCondition.MedicalInformationId);
        if (medicalInformation == null)
        {
            return Result<Unit>.FailureResult("Medical Information not found.");
        }

        medicalInformation.RemoveMedicalCondition(medicalCondition);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Medical Condition removed successfully.");
    }
}
