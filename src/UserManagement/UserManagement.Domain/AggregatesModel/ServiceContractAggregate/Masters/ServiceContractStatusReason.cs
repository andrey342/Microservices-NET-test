using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

public class ServiceContractStatusReason : Entity
{    
    public string Name { get; private set; }
    public ServiceContractStatus ServiceContractStatus { get; private set; }
    public Guid ServiceContractStatusId { get; private set; }

    public ServiceContractStatusReason() { }
    public ServiceContractStatusReason(ServiceContractStatusReason serviceContractStatusReason)
    {
        this.CopyPropertiesTo(serviceContractStatusReason);
    }
}
