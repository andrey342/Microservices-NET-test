using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Events;
public class UserAnimalAddedDomainEvent : IDomainEvent
{
    public Guid UserId { get; }
    public Guid AnimalId { get; }
    public string Name { get; set; }
    public DateTime OccurredOn { get; }

    public UserAnimalAddedDomainEvent(Guid userId, Guid animalId, string name)
    {
        UserId = userId;
        AnimalId = animalId;
        Name = name;
        OccurredOn = DateTime.UtcNow;
    }
}
