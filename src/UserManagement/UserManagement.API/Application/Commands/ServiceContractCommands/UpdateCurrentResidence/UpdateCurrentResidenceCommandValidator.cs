using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateCurrentResidence;

public class UpdateCurrentResidenceCommandValidator : BaseValidator<UpdateCurrentResidenceCommand>
{
    public UpdateCurrentResidenceCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<ServiceContract>(x => x.ActiveResidenceRequest.Id, isRequired: true);
        ValidateGuid<Residence>(x => x.ActiveResidenceRequest.ResidenceId, isRequired: true);
    }
}