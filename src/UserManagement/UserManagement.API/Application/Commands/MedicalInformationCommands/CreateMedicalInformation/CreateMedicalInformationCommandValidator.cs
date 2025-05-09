using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;

public class CreateMedicalInformationCommandValidator : BaseValidator<CreateMedicalInformationCommand>
{
    public CreateMedicalInformationCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<User>(x => x.CreateMedicalInformationRequest.UserId, isRequired: true);
        ValidateGuid<DependencyDegree>(x => x.CreateMedicalInformationRequest.DependencyDegreeId, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.BarthelIndex, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.LawtonIndex, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.PhysicalScaleBSN, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.PhysicalScaleBVD, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.PsychologicalScaleBSN, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.PsychologicalScaleBVD, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.SocialScaleBSN, isRequired: false);
        ValidateDecimal(x => x.CreateMedicalInformationRequest.SocialScaleBVD, isRequired: false);
        ValidateString(x => x.CreateMedicalInformationRequest.Observation, maxLength: 500, isRequired: false);
    }
}
