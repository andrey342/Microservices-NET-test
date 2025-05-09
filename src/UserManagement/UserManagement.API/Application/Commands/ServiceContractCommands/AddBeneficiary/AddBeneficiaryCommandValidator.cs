using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddBeneficiary;

public class AddBeneficiaryCommandValidator : BaseValidator<AddBeneficiaryCommand>
{
    public AddBeneficiaryCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContract>(x => x.BeneficiaryRequest.ServiceContractId, isRequired: true);
        ValidateGuid<User>(x => x.BeneficiaryRequest.UserId, isRequired: false);
    }
}
