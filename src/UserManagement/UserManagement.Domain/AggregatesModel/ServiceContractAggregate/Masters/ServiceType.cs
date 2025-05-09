using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

public class ServiceType : Entity
{
    public string Name { get; private set; }

    public ServiceType() { }

    public ServiceType(string name)
    {
        Name = name;
    }

}
