using UserManagement.API.Application.Common.Models;

namespace UserManagement.API.Application.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}
