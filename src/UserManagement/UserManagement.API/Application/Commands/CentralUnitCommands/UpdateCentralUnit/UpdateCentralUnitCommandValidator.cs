using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;

public class UpdateCentralUnitCommandValidator : BaseValidator<UpdateCentralUnitCommand>
{
    public UpdateCentralUnitCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
    {
        // Campos obligatorios
        ValidateGuid<CentralUnit>(x => x.CentralUnitRequest.Id, isRequired: true);
        ValidateString(x => x.CentralUnitRequest.Code, 20, isRequired: true);
        ValidateString(x => x.CentralUnitRequest.SerialNumber, 50, isRequired: true);
        ValidateString(x => x.CentralUnitRequest.Phone, 15, isRequired: true);
    }
}
