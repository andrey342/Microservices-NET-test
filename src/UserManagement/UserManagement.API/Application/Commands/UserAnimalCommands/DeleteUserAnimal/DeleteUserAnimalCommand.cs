using UserManagement.API.Application.Common.Models;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.DeleteUserAnimal;

public class DeleteUserAnimalCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}
