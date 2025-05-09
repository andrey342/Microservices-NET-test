using UserManagement.API.Application.Commands.IdentificationCommands.CreateIdentification;
using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommandValidator : BaseValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateGuid<User>(x => x.UserRequest.Id, isRequired: true);

        ValidateString(x => x.UserRequest.Name, 50, isRequired: true);
        ValidateString(x => x.UserRequest.Surname1, 50, isRequired: true);
        ValidateGuid<Sex>(x => x.UserRequest.SexId, isRequired: true);
        ValidateGuid<CivilStatus>(x => x.UserRequest.CivilStatusId, isRequired: true);
        ValidateDate(x => x.UserRequest.Birthdate, isRequired: true);

        // Validación del objeto relacionado Identification
        RuleFor(x => x.UserRequest.Identification)
            .NotEmpty().WithMessage($"UserRequest.Identification is required.")
            .SetValidator(new IdentificationValidator(serviceScopeFactory));

        // Campos opcionales con verificación en BD
        ValidateGuid<Identification>(x => x.UserRequest.IdentificationId);
        ValidateGuid<Language>(x => x.UserRequest.LanguageId);
        ValidateGuid<Education>(x => x.UserRequest.EducationId);
        ValidateGuid<DependencyDegree>(x => x.UserRequest.DependencyId);

        // Validaciones opcionales
        ValidateEmail(x => x.UserRequest.Email);
        ValidatePhoneNumber(x => x.UserRequest.Mobile);
        ValidatePhoneNumber(x => x.UserRequest.Phone);
        ValidateString(x => x.UserRequest.Surname2, 50);
        ValidateString(x => x.UserRequest.Appellative, 50);
        ValidateString(x => x.UserRequest.CallTime, 20);
        ValidateString(x => x.UserRequest.Observation, 500);
    }
}
