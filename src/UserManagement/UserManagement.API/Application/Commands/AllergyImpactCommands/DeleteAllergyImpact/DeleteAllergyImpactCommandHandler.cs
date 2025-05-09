using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.DeleteAllergyImpact;

public class DeleteAllergyImpactCommandHandler : IRequestHandler<DeleteAllergyImpactCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    public DeleteAllergyImpactCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<Unit>> Handle(DeleteAllergyImpactCommand request, CancellationToken cancellationToken)
    {
        var allergyImpact = await _userRepository.GetAllergyImpactById(request.Id);

        var medicalInformation = await _userRepository.GetMedicalInformationById(allergyImpact.MedicalInformationId);
        if (medicalInformation == null)
        {
            return Result<Unit>.FailureResult("Medical Condition not found.");
        }

        medicalInformation.RemoveAllergyImpact(allergyImpact);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Allergy Impact removed successfully.");
    }
}