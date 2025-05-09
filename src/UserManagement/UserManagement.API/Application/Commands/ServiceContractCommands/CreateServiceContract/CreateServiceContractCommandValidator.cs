using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract;

public class CreateServiceContractCommandValidator : BaseValidator<CreateServiceContractCommand>
{
    public CreateServiceContractCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<WorkCenter>(x => x.ServiceContractRequest.WorkCenterId, isRequired: true);
        ValidateGuid<ServiceType>(x => x.ServiceContractRequest.ServiceTypeId, isRequired: true);
        ValidateGuid<UserType>(x => x.ServiceContractRequest.UserTypeId, isRequired: true);
        ValidateGuid<UserTypology>(x => x.ServiceContractRequest.UserTypologyId, isRequired: true);
        ValidateGuid<User>(x => x.ServiceContractRequest.UserId, isRequired: false);
    }
}
