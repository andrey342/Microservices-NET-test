using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.UserTypeCommands.CreateUserType;

public class CreateUserTypeCommandValidator : BaseValidator<CreateUserTypeCommand>
{
    public CreateUserTypeCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<WorkCenter>(x => x.WorkCenterId, true);
        ValidateString(x => x.Name, 100, true);
    }
}
