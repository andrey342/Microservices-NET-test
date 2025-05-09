using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Medicine : Entity
{
    public string Name { get; private set; }

    private Medicine() { }

    public Medicine(string name)
    {
        Name = name;
    }
}
