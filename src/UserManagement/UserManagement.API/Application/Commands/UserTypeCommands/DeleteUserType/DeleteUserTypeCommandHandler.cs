using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserTypeCommands.DeleteUserType;

public class DeleteUserTypeCommandHandler : IRequestHandler<DeleteUserTypeCommand, Result<Guid>>
{
    private readonly IUserTypeRepository _userTypeRepository;

    public DeleteUserTypeCommandHandler(IUserTypeRepository userTypeRepository)
    {
        _userTypeRepository = userTypeRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteUserTypeCommand request, CancellationToken cancellationToken)
    {
        var userType = await _userTypeRepository.GetByIdAsync(request.Id);

        _userTypeRepository.Delete(userType);

        await _userTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}
