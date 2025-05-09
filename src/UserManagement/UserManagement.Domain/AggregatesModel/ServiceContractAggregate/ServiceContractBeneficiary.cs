using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate
{
    public class ServiceContractBeneficiary : Entity
    {
        public Guid ServiceContractId { get; private set; }
        public ServiceContract ServiceContract { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public ServiceContractBeneficiary() { }

        public ServiceContractBeneficiary(ServiceContractBeneficiary serviceContractBeneficiary)
        {
            this.CopyPropertiesTo(serviceContractBeneficiary);
        }
    }
}
