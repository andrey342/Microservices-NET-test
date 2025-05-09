using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.WorkCenterCommands.UpdateWorkCenter;

public class UpdateWorkCenterCommandValidator : BaseValidator<UpdateWorkCenterCommand>
{
    public UpdateWorkCenterCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateString(x => x.Name, 100, true);
    }
}
