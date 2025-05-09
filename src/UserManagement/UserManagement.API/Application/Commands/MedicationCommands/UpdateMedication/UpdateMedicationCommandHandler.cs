using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicationCommands.UpdateMedication;

public class UpdateMedicationCommandHandler : IRequestHandler<UpdateMedicationCommand, Result<MedicationViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateMedicationCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<MedicationViewModel>> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
    {
        var existingMedication = await _userRepository.GetMedicationById(request.UpdateMedicationRequest.Id);
        if (existingMedication == null)
        {
            return Result<MedicationViewModel>.FailureResult("Medication not found.");
        }
        var medication = _mapper.Map(request.UpdateMedicationRequest, existingMedication);

        existingMedication.Update(medication);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var medicalConditionViewModel = _mapper.Map<MedicationViewModel>(medication);

        return Result<MedicationViewModel>.SuccessResult(medicalConditionViewModel, "Medication updated successfully.");
    }
}

