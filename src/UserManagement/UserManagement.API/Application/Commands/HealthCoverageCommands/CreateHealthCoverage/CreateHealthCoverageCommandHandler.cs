using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;

public class CreateHealthCoverageCommandHandler : IRequestHandler<CreateHealthCoverageCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateHealthCoverageCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateHealthCoverageCommand request, CancellationToken cancellationToken)
    {
        var medicalInformation = await _userRepository.GetMedicalInformationById(request.CreateHealthCoverageRequest.MedicalInformationId);

        var healthCoverage = _mapper.Map<HealthCoverage>(request.CreateHealthCoverageRequest);
        medicalInformation.AddHealthCoverage(healthCoverage);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(healthCoverage.Id);
    }
}