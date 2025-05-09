using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.KeyCommands.DeleteKey;

public class DeleteKeyCommandValidator : BaseValidator<DeleteKeyCommand>
{
    public DeleteKeyCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Key>(x => x.Id, isRequired: true);
    }
}