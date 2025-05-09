using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.Infrastructure.Repositories;

public class AreaRepository : Repository<Area>, IAreaRepository
{
    public AreaRepository(UserContext context) : base(context)
    { }
}