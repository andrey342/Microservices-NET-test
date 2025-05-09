using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.UpdateMedicalCondition;

public class UpdateMedicalConditionCommandValidator : BaseValidator<UpdateMedicalConditionCommand>
{
    public UpdateMedicalConditionCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<MedicalCondition>(x => x.UpdateMedicalConditionRequest.Id, isRequired: true);
        ValidateGuid<MedicalConditionStatus>(x => x.UpdateMedicalConditionRequest.StatusId, isRequired: true);
        ValidateDate(x => x.UpdateMedicalConditionRequest.DiagnosedDate, isRequired: true, isFutureDate: false);
    }
}
