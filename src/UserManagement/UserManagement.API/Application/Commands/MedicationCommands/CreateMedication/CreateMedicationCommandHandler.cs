using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;

public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateMedicationCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
    {
        var medicalInformation = await _userRepository.GetMedicalInformationById(request.CreateMedicationRequest.MedicalInformationId);

        var medication = _mapper.Map<Medication>(request.CreateMedicationRequest);
        medicalInformation.AddMedication(medication);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(medication.Id);
    }
}