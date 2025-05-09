using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class CivilStatus : Entity
{
    public string Name { get; private set; }

    private CivilStatus() { }

    public CivilStatus(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Civil status name cannot be null or empty.");
        Name = name;
    }
}
