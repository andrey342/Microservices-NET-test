using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

namespace UserManagement.API.Application.Commands.KeyCommands.UpdateKey;

public class UpdateKeyCommandValidator : BaseValidator<UpdateKeyCommand>
{
    public UpdateKeyCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Key>(x => x.Request.Id, isRequired: true);
        ValidateGuid<Residence>(x => x.Request.ResidenceId, isRequired: true);
        ValidateGuid<KeyStatus>(x => x.Request.CurrentStatusId, isRequired: true);
        ValidateString(x => x.Request.Name, maxLength: 50, isRequired: true);
        ValidateString(x => x.Request.Code, maxLength: 20, isRequired: false);
        ValidateString(x => x.Request.Description, maxLength: 200, isRequired: false);
    }
}