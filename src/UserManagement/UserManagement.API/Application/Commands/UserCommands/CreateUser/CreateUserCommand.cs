using UserManagement.API.Application.Common.Models;

namespace UserManagement.API.Application.Commands.UserCommands.CreateUser;

public class CreateUserCommand : IRequest<Result<Guid>>
{
    public CreateUserRequest UserRequest { get; private set; }

    public CreateUserCommand(CreateUserRequest userRequest)
    {
        UserRequest = userRequest;
    }
}