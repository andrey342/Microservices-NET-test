using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.UpdateHealthCoverage;

public class UpdateHealthCoverageCommandValidator : BaseValidator<UpdateHealthCoverageCommand>
{
    public UpdateHealthCoverageCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<HealthCoverage>(x => x.UpdateHealthCoverageRequest.Id, isRequired: true);
        ValidateString(x => x.UpdateHealthCoverageRequest.PolicyNumber, maxLength: 50, isRequired: true);
        ValidateString(x => x.UpdateHealthCoverageRequest.CoverageType, maxLength: 50, isRequired: true);
        ValidateDate(x => x.UpdateHealthCoverageRequest.StartDate, isRequired: true);
        ValidateDate(x => x.UpdateHealthCoverageRequest.EndDate, isRequired: false, isFutureDate: true);
    }
}