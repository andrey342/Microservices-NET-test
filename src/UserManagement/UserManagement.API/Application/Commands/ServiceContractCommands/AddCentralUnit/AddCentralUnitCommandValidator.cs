using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddCentralUnit;

public class AddCentralUnitCommandValidator : BaseValidator<AddCentralUnitCommand>
{
    public AddCentralUnitCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContract>(x => x.CentralUnitRequest.ServiceContractId, isRequired: true);
        ValidateGuid<CentralUnit>(x => x.CentralUnitRequest.CentralUnitId, isRequired: false);
    }
}