using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class Allergy : Entity
{
    public string Name { get; private set; }

    private Allergy() { }

    public Allergy(string name)
    {
        Name = name;
    }
}
