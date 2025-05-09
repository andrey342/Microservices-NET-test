using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;

public class CreateCohabitantCommandValidator : BaseValidator<CreateCohabitantCommand>
{
    public CreateCohabitantCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Residence>(x => x.Request.ResidenceId, isRequired: true);
        ValidateGuid<CohabitantType>(x => x.Request.CohabitantTypeId, isRequired: true);
        ValidateString(x => x.Request.Name, maxLength: 50, isRequired: true);
        ValidateString(x => x.Request.Surname1, maxLength: 50, isRequired: true);
        ValidateString(x => x.Request.Surname2, maxLength: 50);
        ValidatePhoneNumber(x => x.Request.Mobile, isRequired: false);
        ValidateString(x => x.Request.Observation, maxLength: 500);
    }
}