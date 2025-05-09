using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContractStatus;

public class UpdateServiceContractStatusCommandValidator : BaseValidator<UpdateServiceContractStatusCommand>
{
    public UpdateServiceContractStatusCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContract>(x => x.UpdateServiceContractStatusRequest.Id, isRequired: true);
        ValidateGuid<ServiceContractStatus>(x => x.UpdateServiceContractStatusRequest.ServiceContractStatusId, isRequired: true);
    }
}
