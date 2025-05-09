using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events;
public class UserCreatedDomainEvent: IDomainEvent
{
    public Guid UserId { get; }
    public string Name { get; }
    public string Email { get; }
    public DateTime OccurredOn { get; }

    public UserCreatedDomainEvent(Guid userId, string name, string email)
    {
        UserId = userId;
        Name = name;
        Email = email;
        OccurredOn = DateTime.UtcNow;
    }
}
