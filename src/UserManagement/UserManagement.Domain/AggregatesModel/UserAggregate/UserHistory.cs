using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate
{
    public class UserHistory : Entity
    {
        public string Type { get; private set; }
        public string Action { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? ServiceContractId { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public UserHistory(string type, string action, string description, Guid userId, Guid? serviceContractId)
        {
            Type = type;
            Action = action;
            Description = description;
            UserId = userId;
            ServiceContractId = serviceContractId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
