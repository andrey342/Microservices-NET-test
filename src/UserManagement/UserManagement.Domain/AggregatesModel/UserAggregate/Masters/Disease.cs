using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Disease : Entity
{
    public string Name { get; private set; }

    private Disease() { }

    public Disease(string name)
    {
        Name = name;
    }
}
