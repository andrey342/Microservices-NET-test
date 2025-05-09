using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;

public class CreateMedicalConditionCommandValidator : BaseValidator<CreateMedicalConditionCommand>
{
    public CreateMedicalConditionCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<MedicalInformation>(x => x.AddMedicalConditionRequest.MedicalInformationId, isRequired: true);
        ValidateGuid<Disease>(x => x.AddMedicalConditionRequest.DiseaseId, isRequired: true);
        ValidateGuid<MedicalConditionStatus>(x => x.AddMedicalConditionRequest.StatusId, isRequired: true);
    }
}