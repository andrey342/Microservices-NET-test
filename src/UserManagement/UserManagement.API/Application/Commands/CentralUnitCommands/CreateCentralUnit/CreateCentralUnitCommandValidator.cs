using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;

public class CreateCentralUnitCommandValidator : BaseValidator<CreateCentralUnitCommand>
{
    public CreateCentralUnitCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateString(x => x.CentralUnitRequest.Code, 20, isRequired: true);
        ValidateString(x => x.CentralUnitRequest.SerialNumber, 50, isRequired: true);
        ValidateString(x => x.CentralUnitRequest.Phone, 15, isRequired: true);
    }
}