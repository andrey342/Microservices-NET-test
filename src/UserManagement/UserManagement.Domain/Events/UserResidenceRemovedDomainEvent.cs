using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events;
public class UserResidenceRemovedDomainEvent : IDomainEvent
{
    public Guid UserId { get; }
    public Guid ResidenceId { get; }
    public DateTime OccurredOn { get; }

    public UserResidenceRemovedDomainEvent(Guid userId, Guid residenceId)
    {
        UserId = userId;
        ResidenceId = residenceId;
        OccurredOn = DateTime.UtcNow;
    }
}
