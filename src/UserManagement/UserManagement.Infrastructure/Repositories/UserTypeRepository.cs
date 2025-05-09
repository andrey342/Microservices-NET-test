namespace UserManagement.Infrastructure.Repositories;

public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
{
    public UserTypeRepository(UserContext context) : base(context)
    { }
}
