namespace UserManagement.Infrastructure.Repositories;

public class PeripheralRepository : Repository<Peripheral>, IPeripheralRepository
{
    public PeripheralRepository(UserContext context) : base(context)
    { }
}
