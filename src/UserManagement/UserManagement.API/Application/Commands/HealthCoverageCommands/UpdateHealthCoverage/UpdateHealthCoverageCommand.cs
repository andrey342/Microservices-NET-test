using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.UpdateHealthCoverage;

public class UpdateHealthCoverageCommand : IRequest<Result<HealthCoverageViewModel>>
{
    public UpdateHealthCoverageRequest UpdateHealthCoverageRequest { get; private set; }

    public UpdateHealthCoverageCommand(UpdateHealthCoverageRequest updateHealthCoverageRequest)
    {
        UpdateHealthCoverageRequest = updateHealthCoverageRequest;
    }
}