using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters
{
    public class KeyStatus : Entity
    {
        public string Name { get; private set; }
        public bool Default { get; private set; }
        public KeyStatus() { }
        public KeyStatus(string name)
        {
            Name = name;
        }

    }
}
