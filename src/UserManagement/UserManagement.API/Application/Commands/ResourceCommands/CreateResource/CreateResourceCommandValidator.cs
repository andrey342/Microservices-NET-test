using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.ResourceCommands.CreateResource;

public class CreateResourceCommandValidator : BaseValidator<CreateResourceCommand>
{
    public CreateResourceCommandValidator(
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
