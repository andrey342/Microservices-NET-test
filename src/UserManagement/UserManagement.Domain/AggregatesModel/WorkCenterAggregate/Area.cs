using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregateModel.WorkCenterAggregate;

public class Area : Entity
{
    public string Name { get; private set; }
    public AreaLevel AreaLevel { get; private set; }
    public Guid AreaLevelId { get; private set; }
    public Guid? ParentId { get; private set; }
    public Area? Parent { get; private set; }
    private List<Area> _childrens = new List<Area>();
    public IReadOnlyCollection<Area> Childrens => _childrens.AsReadOnly();

    public Area() { }

    public Area(Area area)
    {
        this.CopyPropertiesTo(area);
    }

    public void Update(Area area)
    {
        this.CopyPropertiesTo(area);
    }

    public void SetParent(Area area)
    {
        this.Parent = area;
    }
}
