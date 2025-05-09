namespace UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;

public class CreateHealthCoverageCommand : IRequest<Result<Guid>>
{
    public CreateHealthCoverageRequest CreateHealthCoverageRequest { get; private set; }

    public CreateHealthCoverageCommand(CreateHealthCoverageRequest createHealthCoverageRequest)
    {
        CreateHealthCoverageRequest = createHealthCoverageRequest;
    }
}