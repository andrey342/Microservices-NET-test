using UserManagement.Domain.AggregateModel.WorkCenterAggregate;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
public class WorkCenter : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    private WorkCenter() { }

    public WorkCenter(
        Guid id,
        string name
    )
    {
        Id = id;
        Name = name;
    }

    public void Update(
        string name
    )
    {
        Name = name;
    }

    public void Delete()
    {
    }
}
