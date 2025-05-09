using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;

public class CreateAllergyImpactCommandValidator : BaseValidator<CreateAllergyImpactCommand>
{
    public CreateAllergyImpactCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<MedicalInformation>(x => x.CreateAllergyImpactRequest.MedicalInformationId, isRequired: true);
        ValidateGuid<Allergy>(x => x.CreateAllergyImpactRequest.AllergyId, isRequired: true);
        ValidateGuid<AllergySeverity>(x => x.CreateAllergyImpactRequest.SeverityId, isRequired: true);
        ValidateString(x => x.CreateAllergyImpactRequest.Reaction, maxLength: 500, isRequired: false);
    }
}