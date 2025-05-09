using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.ResourceCommands.UpdateResource;

public class UpdateResourceCommandValidator : BaseValidator<UpdateResourceCommand>
{
    public UpdateResourceCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateString(x => x.Name, 100, true);
        ValidateGuid<WorkCenter>(x => x.WorkCenterId, true);

        ValidatePhoneNumber(x => x.PhoneNumbers.HomePhone, isRequired: false);
        ValidatePhoneNumber(x => x.PhoneNumbers.MobilePhone, isRequired: false);
    }
}