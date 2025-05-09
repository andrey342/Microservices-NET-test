using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class UserResidence : Entity
{
    public Guid UserId { get; private set; }
    public Guid ResidenceId { get; private set; }
    public DateTime AddedAt { get; private set; }

    private UserResidence() { } // Constructor para EF

    public UserResidence(Guid userId, Guid residenceId)
    {
        UserId = userId;
        ResidenceId = residenceId;
        AddedAt = DateTime.UtcNow;
    }

    public void UpdateResidence(Guid newResidenceId)
    {
        ResidenceId = newResidenceId;
    }
}
