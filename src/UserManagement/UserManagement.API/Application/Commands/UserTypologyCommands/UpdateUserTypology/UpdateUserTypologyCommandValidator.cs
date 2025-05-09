using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.UserTypologyCommands.UpdateUserTypology;

public class UpdateUserTypologyCommandValidator : BaseValidator<UpdateUserTypologyCommand>
{
    public UpdateUserTypologyCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<UserTypology>(x => x.Id, true);
        ValidateGuid<WorkCenter>(x => x.WorkCenterId, true);
        ValidateString(x => x.Name, 100, true);
    }
}