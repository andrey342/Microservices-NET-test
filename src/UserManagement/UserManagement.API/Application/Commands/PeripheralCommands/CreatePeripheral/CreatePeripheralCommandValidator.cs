using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;

public class CreatePeripheralCommandValidator : BaseValidator<CreatePeripheralCommand>
{
    public CreatePeripheralCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateString(x => x.PeripheralRequest.Code, 20, isRequired: true);
        ValidateString(x => x.PeripheralRequest.SerialNumber, 50, isRequired: true);
    }
}