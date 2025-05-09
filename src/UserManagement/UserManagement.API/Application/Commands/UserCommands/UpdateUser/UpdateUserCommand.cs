using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommand : IRequest<Result<BasicUserViewModel>>
{
    public UpdateUserRequest UserRequest { get; set; }

    public UpdateUserCommand(UpdateUserRequest userRequest)
    {
        UserRequest = userRequest;
    }
}
