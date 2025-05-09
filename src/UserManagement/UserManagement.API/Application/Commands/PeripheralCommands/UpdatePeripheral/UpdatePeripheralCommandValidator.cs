using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;

public class UpdatePeripheralCommandValidator : BaseValidator<UpdatePeripheralCommand>
{
    public UpdatePeripheralCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateGuid<Peripheral>(x => x.PeripheralRequest.Id, isRequired: true);
        ValidateString(x => x.PeripheralRequest.Code, 20, isRequired: true);
        ValidateString(x => x.PeripheralRequest.SerialNumber, 50, isRequired: true);
    }
}