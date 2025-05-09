using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;

public class UpdateMedicalConditionCommandHandler : IRequestHandler<UpdateMedicalConditionCommand, Result<MedicalConditionViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateMedicalConditionCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<MedicalConditionViewModel>> Handle(UpdateMedicalConditionCommand request, CancellationToken cancellationToken)
    {
        var existingMedicalCondition = await _userRepository.GetMedicalConditionById(request.UpdateMedicalConditionRequest.Id);
        if (existingMedicalCondition == null)
        {
            return Result<MedicalConditionViewModel>.FailureResult("Medical Condition not found.");
        }
        var medicalCondition = _mapper.Map(request.UpdateMedicalConditionRequest, existingMedicalCondition);

        existingMedicalCondition.Update(medicalCondition);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var medicalConditionViewModel = _mapper.Map<MedicalConditionViewModel>(medicalCondition);

        return Result<MedicalConditionViewModel>.SuccessResult(medicalConditionViewModel, "Medical Condition updated successfully.");
    }
}
