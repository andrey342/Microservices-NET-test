using UserManagement.API.Application.Queries.UserQueries;

public class UserServices(
    IMediator mediator,
    IUserQueries queries,
//    IIdentityService identityService,
    ILogger<UserServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<UserServices> Logger { get; } = logger;
    public IUserQueries Queries { get; } = queries;
//    public IIdentityService IdentityService { get; } = identityService;
}
