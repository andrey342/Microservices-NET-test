using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
public class MedicalConditionStatus : Entity
{
    public string Name { get; private set; }

    private MedicalConditionStatus() { }

    public MedicalConditionStatus(string name)
    {
        Name = name;
    }
}
