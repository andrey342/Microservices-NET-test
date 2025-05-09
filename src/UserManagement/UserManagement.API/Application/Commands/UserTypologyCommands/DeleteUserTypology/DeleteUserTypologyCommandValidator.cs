using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.UserTypologyCommands.DeleteUserTypology;

public class DeleteUserTypologyCommandValidator : BaseValidator<DeleteUserTypologyCommand>
{
    public DeleteUserTypologyCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<UserTypology>(x => x.Id, true);
    }
}
