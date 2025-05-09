using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregateModel.WorkCenterAggregate;

public class AreaLevel : Entity
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public Guid WorkCenterId { get; private set; }
    private List<Area> _areas = new List<Area>();
    public IReadOnlyCollection<Area> Areas => _areas.AsReadOnly();
    public AreaLevel() { }

    public AreaLevel(AreaLevel areaLevel)
    {
        this.CopyPropertiesTo(areaLevel);
    }

    public void Update(AreaLevel areaLevel)
    {
        this.CopyPropertiesTo(areaLevel);
    }
}