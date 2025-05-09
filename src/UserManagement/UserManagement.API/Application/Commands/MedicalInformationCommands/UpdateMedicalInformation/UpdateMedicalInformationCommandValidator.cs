using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.UpdateMedicalInformation;

public class UpdateMedicalInformationCommandValidator : BaseValidator<UpdateMedicalInformationCommand>
{
    public UpdateMedicalInformationCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<User>(x => x.UpdateMedicalInformationRequest.UserId, isRequired: true);
        ValidateGuid<DependencyDegree>(x => x.UpdateMedicalInformationRequest.DependencyDegreeId, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.BarthelIndex, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.LawtonIndex, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.PhysicalScaleBSN, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.PhysicalScaleBVD, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.PsychologicalScaleBSN, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.PsychologicalScaleBVD, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.SocialScaleBSN, isRequired: false);
        ValidateDecimal(x => x.UpdateMedicalInformationRequest.SocialScaleBVD, isRequired: false);
        ValidateString(x => x.UpdateMedicalInformationRequest.Observation, maxLength: 500, isRequired: false);
    }
}