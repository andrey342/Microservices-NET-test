using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContract;

public class UpdateServiceContractCommandValidator : BaseValidator<UpdateServiceContractCommand>
{
    public UpdateServiceContractCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContract>(x => x.ServiceContractRequest.Id, isRequired: true);
        ValidateGuid<User>(x => x.ServiceContractRequest.UserId, isRequired: true);
        ValidateGuid<WorkCenter>(x => x.ServiceContractRequest.WorkCenterId, isRequired: true);
        ValidateGuid<ServiceContractStatus>(x => x.ServiceContractRequest.CurrentStatusId, isRequired: true);
        ValidateGuid<ServiceContractStatusReason>(x => x.ServiceContractRequest.ServiceContractStatusReasonId, isRequired: false);
        ValidateGuid<ServiceType>(x => x.ServiceContractRequest.ServiceTypeId, isRequired: true);
        ValidateGuid<UserType>(x => x.ServiceContractRequest.UserTypeId, isRequired: true);
        ValidateGuid<UserTypology>(x => x.ServiceContractRequest.UserTypologyId, isRequired: true);
    }
}