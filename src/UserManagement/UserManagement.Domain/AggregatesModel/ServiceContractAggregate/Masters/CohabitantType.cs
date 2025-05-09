using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

public class CohabitantType : Entity
{
    public string Name { get; private set; }

    private CohabitantType() { }

    public CohabitantType(string name)
    {
        Name = name;
    }
}