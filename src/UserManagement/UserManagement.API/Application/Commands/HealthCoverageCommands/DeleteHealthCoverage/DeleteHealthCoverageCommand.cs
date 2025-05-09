namespace UserManagement.API.Application.Commands.HealthCoverageCommands.DeleteHealthCoverage;

public class DeleteHealthCoverageCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public DeleteHealthCoverageCommand(Guid id)
    {
        Id = id;
    }
}