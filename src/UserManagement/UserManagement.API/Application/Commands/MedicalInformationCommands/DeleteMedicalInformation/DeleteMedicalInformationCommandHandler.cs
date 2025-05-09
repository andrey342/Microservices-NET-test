using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.DeleteMedicalInformation;

public class DeleteMedicalInformationCommandHandler : IRequestHandler<DeleteMedicalInformationCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;

    public DeleteMedicalInformationCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteMedicalInformationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        user.RemoveMedicalInformation();
        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Result<Unit>.SuccessResult(Unit.Value, "Medical Information deleted successfully.");
    }
}