using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.CreateUserAnimal;

public class CreateUserAnimalCommand : IRequest<Result<FullUserAnimalViewModel>>
{
    public CreateUserAnimalRequest UserAnimalCreateDto { get; set; }

    public CreateUserAnimalCommand(CreateUserAnimalRequest userAnimalCreateDto)
    {
        UserAnimalCreateDto = userAnimalCreateDto;
    }
}
