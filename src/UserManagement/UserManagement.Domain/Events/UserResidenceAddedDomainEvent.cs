using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events;
public class UserResidenceAddedDomainEvent : IDomainEvent
{
    public Guid UserId { get; }
    public Guid ResidenceId { get; }
    public DateTime OccurredOn { get; }

    public UserResidenceAddedDomainEvent(Guid userId, Guid residenceId)
    {
        UserId = userId;
        ResidenceId = residenceId;
        OccurredOn = DateTime.UtcNow;
    }
}
