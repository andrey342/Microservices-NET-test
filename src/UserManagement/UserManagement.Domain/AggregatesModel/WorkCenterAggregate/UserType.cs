using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

public class UserType: Entity
{
    public string Name { get; private set; }
    public Guid WorkCenterId { get; private set; }

    public UserType() { }

    public UserType(UserType userType)
    {
        this.CopyPropertiesTo(userType);
    }
}
