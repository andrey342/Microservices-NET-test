using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Commands.CohabitantCommands.DeleteCohabitant;

public class DeleteCohabitantValidator : BaseValidator<DeleteCohabitantCommand>
{
    public DeleteCohabitantValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Cohabitant>(x => x.Id, isRequired: true);
    }
}