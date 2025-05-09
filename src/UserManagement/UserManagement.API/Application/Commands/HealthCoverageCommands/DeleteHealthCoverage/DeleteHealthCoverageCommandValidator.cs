using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.DeleteHealthCoverage;

public class DeleteHealthCoverageCommandValidator : BaseValidator<DeleteHealthCoverageCommand>
{
    public DeleteHealthCoverageCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<HealthCoverage>(x => x.Id, isRequired: true);
    }
}