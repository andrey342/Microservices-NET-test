using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

public class UserTypology : Entity
{
    public string Name { get; private set; }
    public Guid WorkCenterId { get; private set; }

    public UserTypology() { }

    public UserTypology(UserTypology userTypology)
    {
        this.CopyPropertiesTo(userTypology);
    }
}
