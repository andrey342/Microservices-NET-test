using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

public class ServiceContractStatus : Entity
{
    public string Name { get; private set; }
    public bool Default { get; private set; }

    public ServiceContractStatus() { }

    public ServiceContractStatus(string name)
    {
        Name = name;
    }
}
