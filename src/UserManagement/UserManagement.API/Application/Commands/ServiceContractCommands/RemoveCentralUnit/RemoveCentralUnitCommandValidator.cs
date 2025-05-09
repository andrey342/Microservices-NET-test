using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemoveCentralUnit;

public class RemoveCentralUnitCommandValidator : BaseValidator<RemoveCentralUnitCommand>
{
    public RemoveCentralUnitCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContractCentralUnit>(x => x.Id, isRequired: true);
    }
}
