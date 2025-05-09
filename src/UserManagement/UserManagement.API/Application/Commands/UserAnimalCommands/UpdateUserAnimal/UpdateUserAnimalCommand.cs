using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.UpdateUserAnimal;

public class UpdateUserAnimalCommand : IRequest<Result<BasicUserViewModel>>
{
    public UpdateUserAnimalRequest UserAnimalUpdate { get; set; }

    public UpdateUserAnimalCommand(UpdateUserAnimalRequest userAnimalUpdateDto)
    {
        UserAnimalUpdate = userAnimalUpdateDto;
    }
}
