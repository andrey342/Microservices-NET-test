using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;

public class CreateMedicalInformationCommandHandler : IRequestHandler<CreateMedicalInformationCommand, Result<MedicalInformationViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public CreateMedicalInformationCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<Result<MedicalInformationViewModel>> Handle(CreateMedicalInformationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.CreateMedicalInformationRequest.UserId);
       
        var medicalInformation = _mapper.Map<MedicalInformation>(request.CreateMedicalInformationRequest);
        user.AssignMedicalInformation(_mapper.Map<MedicalInformation>(request.CreateMedicalInformationRequest));
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var medicalInformationViewModel = _mapper.Map<MedicalInformationViewModel>(medicalInformation);
        return Result<MedicalInformationViewModel>.SuccessResult(medicalInformationViewModel, "Create medical information successfully.");
    }
}
