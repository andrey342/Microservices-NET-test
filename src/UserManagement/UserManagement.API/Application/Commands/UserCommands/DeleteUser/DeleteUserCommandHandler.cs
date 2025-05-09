using System.Text.Json;
using UserManagement.API.Application.Common.Models;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserCommands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        _userRepository.Delete(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Result<Unit>.SuccessResult(Unit.Value, "User deleted successfully.");
    }
}
