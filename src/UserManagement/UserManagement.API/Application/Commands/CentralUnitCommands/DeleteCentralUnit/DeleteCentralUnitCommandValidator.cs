using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.DeleteCentralUnit;

public class DeleteCentralUnitCommandValidator : BaseValidator<DeleteCentralUnitCommand>
{
    public DeleteCentralUnitCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<CentralUnit>(x => x.Id, true);
    }
}
