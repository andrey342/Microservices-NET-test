using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.UserCommands.DeleteUser;
public class DeleteUserCommandValidator : BaseValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateGuid<User>(x => x.Id, isRequired: true);
    }
}
