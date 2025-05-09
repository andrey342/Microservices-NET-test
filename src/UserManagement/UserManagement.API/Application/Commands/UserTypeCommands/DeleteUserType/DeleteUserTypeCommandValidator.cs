using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.UserTypeCommands.DeleteUserType;

public class DeleteUserTypeCommandValidator : BaseValidator<DeleteUserTypeCommand>
{
    public DeleteUserTypeCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<UserType>(x => x.Id, true);
    }
}