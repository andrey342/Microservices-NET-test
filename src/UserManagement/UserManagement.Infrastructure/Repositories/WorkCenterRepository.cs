namespace UserManagement.Infrastructure.Repositories;
public class WorkCenterRepository : Repository<WorkCenter>, IWorkCenterRepository
{
    public WorkCenterRepository(UserContext context) : base(context)
    {}
}
