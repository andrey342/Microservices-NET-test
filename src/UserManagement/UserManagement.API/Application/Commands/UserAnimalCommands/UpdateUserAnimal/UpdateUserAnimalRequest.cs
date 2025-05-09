using UserManagement.API.Application.Commands.UserAnimalCommands.CreateUserAnimal;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.UpdateUserAnimal;

public class UpdateUserAnimalRequest : CreateUserAnimalRequest
{
    public Guid Id { get; set; }
}
