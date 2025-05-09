using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.DeleteHealthCoverage;

public class DeleteHealthCoverageCommandHandler : IRequestHandler<DeleteHealthCoverageCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    public DeleteHealthCoverageCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<Unit>> Handle(DeleteHealthCoverageCommand request, CancellationToken cancellationToken)
    {
        var healthCoverage = await _userRepository.GetHealthCoverageById(request.Id);

        var medicalInformation = await _userRepository.GetMedicalInformationById(healthCoverage.MedicalInformationId);
        if (medicalInformation == null)
        {
            return Result<Unit>.FailureResult("Medical Information not found.");
        }

        medicalInformation.RemoveHealthCoverage(healthCoverage);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Health coverage removed successfully.");
    }
}
