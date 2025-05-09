using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class DependencyDegree : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    private DependencyDegree() { }

    public DependencyDegree(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Dependency degree name cannot be null or empty.");
        Name = name;
        Description = description;
    }
}
