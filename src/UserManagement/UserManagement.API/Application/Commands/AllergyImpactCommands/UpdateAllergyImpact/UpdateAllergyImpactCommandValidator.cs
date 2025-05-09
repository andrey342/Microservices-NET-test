using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.UpdateAllergyImpact;

public class UpdateAllergyImpactCommandValidator : BaseValidator<UpdateAllergyImpactCommand>
{
    public UpdateAllergyImpactCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<AllergyImpact>(x => x.UpdateAllergyImpactRequest.Id, isRequired: true);
        ValidateGuid<AllergySeverity>(x => x.UpdateAllergyImpactRequest.SeverityId, isRequired: true);
        ValidateString(x => x.UpdateAllergyImpactRequest.Reaction, isRequired: false);
    }
}
