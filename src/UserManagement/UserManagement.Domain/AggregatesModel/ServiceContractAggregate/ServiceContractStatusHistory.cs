using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class ServiceContractStatusHistory : Entity
{
    public ServiceContract ServiceContract { get; private set; }
    public Guid ServiceContractId { get; private set; }
    public ServiceContractStatus ServiceContractStatus { get; private set; }
    public Guid ServiceContractStatusId { get; private set; }
    public ServiceContractStatusReason ServiceContractStatusReason { get; private set; }
    public Guid ServiceContractStatusReasonId { get; private set; }
    public DateTime Date { get; private set; }

    public ServiceContractStatusHistory() { }

    public ServiceContractStatusHistory(Guid serviceContractId, Guid serviceContractStatusId, Guid serviceContractStatusReasonId, DateTime date)
    {
        ServiceContractId = serviceContractId;
        ServiceContractStatusId = serviceContractStatusId;
        ServiceContractStatusReasonId = serviceContractStatusReasonId;
        Date = date;
    }
}
