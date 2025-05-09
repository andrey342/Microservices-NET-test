using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.PreferredProfessionalCommands.UpdatePreferredProfessional;

public class UpdatePreferredProfessionalCommandValidator : BaseValidator<UpdatePreferredProfessionalCommand>
{
    public UpdatePreferredProfessionalCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validar que el ProfessionalId sea obligatorio
        RuleFor(x => x.ProfessionalId)
            .NotEmpty().WithMessage("ProfessionalId es obligatorio.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("ProfessionalId debe ser un GUID válido.");

        // Validar que el Name sea obligatorio y tenga una longitud máxima de 100 caracteres
        ValidateString(x => x.Name, maxLength: 100, isRequired: true);

        // Validar que el Surname1 sea obligatorio y tenga una longitud máxima de 100 caracteres
        ValidateString(x => x.Surname1, maxLength: 100, isRequired: true);

        // Validar que el Surname2 sea opcional y tenga una longitud máxima de 100 caracteres
        ValidateString(x => x.Surname2, maxLength: 100, isRequired: false);

    }
}
