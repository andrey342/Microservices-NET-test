using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.AreaLevelCommands.DeleteAreaLevel;

public class DeleteAreaLevelCommandValidator : BaseValidator<DeleteAreaLevelCommand>
{
    public DeleteAreaLevelCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<AreaLevel>(x => x.Id, true);
    }
}