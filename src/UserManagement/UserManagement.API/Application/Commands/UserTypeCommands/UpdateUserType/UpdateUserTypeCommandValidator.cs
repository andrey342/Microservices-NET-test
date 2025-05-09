using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.UserTypeCommands.UpdateUserType;

public class UpdateUserTypeCommandValidator : BaseValidator<UpdateUserTypeCommand>
{
    public UpdateUserTypeCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<UserType>(x => x.Id, true);
        ValidateGuid<WorkCenter>(x => x.WorkCenterId, true);
        ValidateString(x => x.Name, 100, true);
    }
}