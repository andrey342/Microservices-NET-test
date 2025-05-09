using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Education : Entity
{
    public string Name { get; private set; }

    private Education() { }

    public Education(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Education name cannot be null or empty.");
        Name = name;
    }
}
