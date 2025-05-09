using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class UserAnimal : Entity
{
    public Guid UserId { get; private set; }
    public Guid AnimalId { get; private set; }
    public string Name { get; private set; }
    public Animal Animal { get; private set; }

    private UserAnimal() { }

    public UserAnimal(Guid userId, Guid animalId, string name)
    {
        UserId = userId;
        AnimalId = animalId;
        Name = name;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Animal name cannot be empty.");
        }

        Name = newName;
    }
}
