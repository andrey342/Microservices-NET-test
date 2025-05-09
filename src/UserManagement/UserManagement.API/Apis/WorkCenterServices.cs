using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Apis;

public class WorkCenterServices(
    IUserQueries queries,
    ILogger<WorkCenterServices> logger)
{
    public ILogger<WorkCenterServices> Logger { get; } = logger;
    public IUserQueries Queries { get; } = queries;
}