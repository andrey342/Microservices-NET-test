namespace UserManagement.Infrastructure.Repositories;

public class ResourceRepository : Repository<Resource>, IResourceRepository
{
    public ResourceRepository(UserContext context) : base(context)
    { }
}