using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events;
public class UserResidenceUpdatedDomainEvent : IDomainEvent
{
    public Guid UserId { get; }
    public Guid OldResidenceId { get; }
    public Guid NewResidenceId { get; }
    public DateTime OccurredOn { get; }

    public UserResidenceUpdatedDomainEvent(Guid userId, Guid oldResidenceId, Guid newResidenceId)
    {
        UserId = userId;
        OldResidenceId = oldResidenceId;
        NewResidenceId = newResidenceId;
        OccurredOn = DateTime.UtcNow;
    }
}
