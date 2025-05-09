using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;

public class CreateHealthCoverageCommandValidator : BaseValidator<CreateHealthCoverageCommand>
{
    public CreateHealthCoverageCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<MedicalInformation>(x => x.CreateHealthCoverageRequest.MedicalInformationId, isRequired: true);
        ValidateString(x => x.CreateHealthCoverageRequest.Provider, maxLength: 100, isRequired: true);
        ValidateString(x => x.CreateHealthCoverageRequest.PolicyNumber, maxLength: 50, isRequired: true);
        ValidateString(x => x.CreateHealthCoverageRequest.CoverageType, maxLength: 50, isRequired: true);
        ValidateDate(x => x.CreateHealthCoverageRequest.StartDate, isRequired: true);
        ValidateDate(x => x.CreateHealthCoverageRequest.EndDate, isRequired: false, isFutureDate: true);
    }
}