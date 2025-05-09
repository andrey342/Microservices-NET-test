using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;

public class CreateAllergyImpactCommandHandler : IRequestHandler<CreateAllergyImpactCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateAllergyImpactCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateAllergyImpactCommand request, CancellationToken cancellationToken)
    {
        var medicalInformation = await _userRepository.GetMedicalInformationById(request.CreateAllergyImpactRequest.MedicalInformationId);

        var allergyImpact = _mapper.Map<AllergyImpact>(request.CreateAllergyImpactRequest);
        medicalInformation.AddAllergyImpact(allergyImpact);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(allergyImpact.Id);
    }
}