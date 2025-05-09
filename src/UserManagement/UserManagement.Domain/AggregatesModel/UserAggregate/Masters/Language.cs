using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Language : Entity
{
    public string Name { get; private set; }

    private Language() { }

    public Language(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Language name cannot be null or empty.");
        Name = name;
    }
}
