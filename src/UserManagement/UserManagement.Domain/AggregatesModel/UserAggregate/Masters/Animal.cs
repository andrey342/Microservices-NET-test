using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Animal : Entity
{
    public string Name { get; private set; }

    private Animal() { }

    public Animal(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Animal name cannot be null or empty.");
        Name = name;
    }
}
