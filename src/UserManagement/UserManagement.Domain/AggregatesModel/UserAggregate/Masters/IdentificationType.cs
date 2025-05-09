using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class IdentificationType : Entity
{
    public string Name { get; private set; }

    private IdentificationType() { }

    public IdentificationType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Identification type name cannot be null or empty.");
        Name = name;
    }
}
