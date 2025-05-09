using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;

public class CreateMedicalConditionCommandHandler : IRequestHandler<CreateMedicalConditionCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateMedicalConditionCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateMedicalConditionCommand request, CancellationToken cancellationToken)
    {
        var medicalInformation = await _userRepository.GetMedicalInformationById(request.AddMedicalConditionRequest.MedicalInformationId);

        var medicalCondition = _mapper.Map<MedicalCondition>(request.AddMedicalConditionRequest);
        medicalInformation.AddMedicalCondition(medicalCondition);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(medicalCondition.Id);
    }
}