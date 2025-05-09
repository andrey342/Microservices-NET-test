using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.UserTypologyCommands.CreateUserTypology;

public class CreateUserTypologyCommandValidator : BaseValidator<CreateUserTypologyCommand>
{
    public CreateUserTypologyCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<WorkCenter>(x => x.WorkCenterId, true);
        ValidateString(x => x.Name, 100, true);
    }
}