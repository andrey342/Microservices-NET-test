using UserManagement.API.Application.Commands.UserCommands.CreateUser;

namespace UserManagement.API.Application.Commands.UserCommands.UpdateUser;
public class UpdateUserRequest : CreateUserRequest
{
    public Guid Id { get; set; }
}
