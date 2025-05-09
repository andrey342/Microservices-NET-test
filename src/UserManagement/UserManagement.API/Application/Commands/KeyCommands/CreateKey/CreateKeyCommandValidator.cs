using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.KeyCommands.CreateKey;

public class CreateKeyCommandValidator : BaseValidator<CreateKeyCommand>
{
    public CreateKeyCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Residence>(x => x.Request.ResidenceId, isRequired: true);
        ValidateString(x => x.Request.Name, maxLength: 50, isRequired: true);
        ValidateString(x => x.Request.Code, maxLength: 20, isRequired: false);
        ValidateString(x => x.Request.Description, maxLength: 200, isRequired: false);
    }
}