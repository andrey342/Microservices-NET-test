namespace UserManagement.Infrastructure.Repositories;
public class Repository<T> : IGenericRepository<T> where T : class
{
    protected readonly UserContext _context;

    public Repository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id) != null;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
