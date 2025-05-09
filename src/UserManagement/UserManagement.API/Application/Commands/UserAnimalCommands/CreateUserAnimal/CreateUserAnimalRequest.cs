namespace UserManagement.API.Application.Commands.UserAnimalCommands.CreateUserAnimal;

public class CreateUserAnimalRequest
{
    public Guid UserId { get; set; }
    public Guid AnimalId { get; set; }
    public string Name { get; set; } = null!;
}
