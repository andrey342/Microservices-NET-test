using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.Infrastructure.Repositories;

public class AreaLevelRepository : Repository<AreaLevel>, IAreaLevelRepository
{
    public AreaLevelRepository(UserContext context) : base(context)
    { }
}