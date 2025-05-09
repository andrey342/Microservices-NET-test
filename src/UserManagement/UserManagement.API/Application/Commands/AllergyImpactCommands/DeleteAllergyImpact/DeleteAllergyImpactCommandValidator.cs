using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.DeleteAllergyImpact;

public class DeleteAllergyImpactCommandValidator : BaseValidator<DeleteAllergyImpactCommand>
{
    public DeleteAllergyImpactCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<AllergyImpact>(x => x.Id, isRequired: true);
    }
}