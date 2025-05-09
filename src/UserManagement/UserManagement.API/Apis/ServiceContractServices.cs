using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Apis;

public class ServiceContractServices(
    IMediator mediator,
    IServiceContractQueries queries,
    ILogger<ServiceContractServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<ServiceContractServices> Logger { get; } = logger;
    public IServiceContractQueries Queries { get; } = queries;
}