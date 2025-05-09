namespace UserManagement.Infrastructure.Repositories;

public class UserTypologyRepository : Repository<UserTypology>, IUserTypologyRepository
{
    public UserTypologyRepository(UserContext context) : base(context)
    { }
}