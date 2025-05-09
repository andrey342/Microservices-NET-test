namespace UserManagement.Infrastructure.Repositories;

public class CentralUnitRepository : Repository<CentralUnit>, ICentralUnitRepository
{
    public CentralUnitRepository(UserContext context) : base(context)
    { }
}
