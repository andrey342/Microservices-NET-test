using UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.UpdateHealthCoverage;

public class UpdateHealthCoverageCommandHandler : IRequestHandler<UpdateHealthCoverageCommand, Result<HealthCoverageViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateHealthCoverageCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<HealthCoverageViewModel>> Handle(UpdateHealthCoverageCommand request, CancellationToken cancellationToken)
    {
        var existingHealthCoverage = await _userRepository.GetHealthCoverageById(request.UpdateHealthCoverageRequest.Id);
        if (existingHealthCoverage == null)
        {
            return Result<HealthCoverageViewModel>.FailureResult("Medical Condition not found.");
        }
        var healthCoverage = _mapper.Map(request.UpdateHealthCoverageRequest, existingHealthCoverage);

        existingHealthCoverage.Update(healthCoverage);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var healthCoverageViewModel = _mapper.Map<HealthCoverageViewModel>(healthCoverage);

        return Result<HealthCoverageViewModel>.SuccessResult(healthCoverageViewModel, "Health Coverage updated successfully.");
    }
}
