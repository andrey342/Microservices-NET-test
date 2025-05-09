using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.UpdateAllergyImpact;

public class UpdateAllergyImpactCommandHandler : IRequestHandler<UpdateAllergyImpactCommand, Result<AllergyImpactViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateAllergyImpactCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<AllergyImpactViewModel>> Handle(UpdateAllergyImpactCommand request, CancellationToken cancellationToken)
    {
        var existingAllergyImpact = await _userRepository.GetAllergyImpactById(request.UpdateAllergyImpactRequest.Id);

        var allergyImpact = _mapper.Map(request.UpdateAllergyImpactRequest, existingAllergyImpact);

        existingAllergyImpact.Update(allergyImpact);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var medicalConditionViewModel = _mapper.Map<AllergyImpactViewModel>(allergyImpact);

        return Result<AllergyImpactViewModel>.SuccessResult(medicalConditionViewModel, "Allergy Impact updated successfully.");
    }
}
