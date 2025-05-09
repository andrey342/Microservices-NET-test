using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.WorkCenterCommands.CreateWorkCenter;

public class CreateWorkCenterCommandValidator : BaseValidator<CreateWorkCenterCommand>
{
    public CreateWorkCenterCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateString(x => x.Name, 100, true);
    }
}
