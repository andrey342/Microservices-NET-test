using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    IUnitOfWork UnitOfWork { get; }
    Task<bool> ExistsAsync(Guid id);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
