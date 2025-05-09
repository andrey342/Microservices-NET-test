using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.PreferredProfessionalCommands.CreatePreferredProfessional;

public class PreferredProfessionalValidator : BaseValidator<CreatePreferredProfessionalRequest>
{
    public PreferredProfessionalValidator(
    IServiceScopeFactory serviceScopeFactory)
    : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateString(x => x.Name, 100, isRequired: true);
        ValidateString(x => x.Surname1, 100, isRequired: true);
        // Opcionales
        ValidateString(x => x.Surname2, isRequired: false);
    }
}
