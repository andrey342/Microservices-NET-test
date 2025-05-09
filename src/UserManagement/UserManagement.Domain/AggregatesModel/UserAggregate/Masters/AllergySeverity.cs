using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class AllergySeverity : Entity
{
    public string Name { get; private set; }
    public int Order { get; private set; }

    private AllergySeverity() { }

    public AllergySeverity(string name, int order)
    {
        Name = name;
        Order = order;
    }
}
