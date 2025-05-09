using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.RemoveMedicalCondition;

public class DeleteMedicalConditionCommandValidator : BaseValidator<DeleteMedicalConditionCommand>
{
    public DeleteMedicalConditionCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<MedicalCondition>(x => x.Id, isRequired: true);
    }
}