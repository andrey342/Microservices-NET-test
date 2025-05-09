using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.UpdateMedicalInformation;

public class UpdateMedicalInformationCommandHandler : IRequestHandler<UpdateMedicalInformationCommand, Result<MedicalInformationViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateMedicalInformationCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<MedicalInformationViewModel>> Handle(UpdateMedicalInformationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UpdateMedicalInformationRequest.UserId);

        var medicalInformation = user.MedicalInformation;
        if (medicalInformation == null)
        {
            return Result<MedicalInformationViewModel>.FailureResult("Medical Information not found.");
        }

        // Mapea las propiedades de la solicitud a la entidad
        _mapper.Map(request.UpdateMedicalInformationRequest, medicalInformation);

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var medicalInformationViewModel = _mapper.Map<MedicalInformationViewModel>(medicalInformation);

        return Result<MedicalInformationViewModel>.SuccessResult(medicalInformationViewModel, "Medical Information updated successfully.");
    }
}