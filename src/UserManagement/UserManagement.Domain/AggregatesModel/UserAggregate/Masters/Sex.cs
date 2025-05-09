using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Sex : Entity
{
    public string Name { get; private set; }

    private Sex() { }

    public Sex(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Sex name cannot be null or empty.");
        Name = name;
    }
}
