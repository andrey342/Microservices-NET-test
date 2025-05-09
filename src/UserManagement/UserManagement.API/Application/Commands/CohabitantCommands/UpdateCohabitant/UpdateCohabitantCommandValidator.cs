using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

namespace UserManagement.API.Application.Commands.CohabitantCommands.UpdateCohabitant;

public class UpdateCohabitantCommandValidator : BaseValidator<UpdateCohabitantCommand>
{
    public UpdateCohabitantCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Cohabitant>(x => x.Request.Id, isRequired: true);
        ValidateGuid<Residence>(x => x.Request.ResidenceId, isRequired: true);
        ValidateGuid<CohabitantType>(x => x.Request.CohabitantTypeId, isRequired: true);
        ValidateString(x => x.Request.Name, maxLength: 50, isRequired: true);
        ValidateString(x => x.Request.Surname1, maxLength: 50, isRequired: true);
        ValidateString(x => x.Request.Surname2, maxLength: 50);
        ValidatePhoneNumber(x => x.Request.Mobile, isRequired: false);
        ValidateString(x => x.Request.Observation, maxLength: 500);
    }
}