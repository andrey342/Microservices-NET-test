using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemoveBeneficiary;

public class RemoveBeneficiaryCommandValidator : BaseValidator<RemoveBeneficiaryCommand>
{
    public RemoveBeneficiaryCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContractBeneficiary>(x => x.Id, isRequired: true);
    }
}