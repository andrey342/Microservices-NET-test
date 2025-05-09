using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events;
internal class UserDeletedDomainEvent: IDomainEvent
{
    public Guid UserId { get; }
    public DateTime OccurredOn { get; }

    public UserDeletedDomainEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}
