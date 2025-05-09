using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.AreaLevelCommands.UpdateAreaLevel;

public class UpdateAreaLevelCommandValidator : BaseValidator<UpdateAreaLevelCommand>
{
    public UpdateAreaLevelCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<AreaLevel>(x => x.Id, true);
        ValidateString(x => x.Name, 100, true);
        ValidatePositiveNumber(x => x.Level, true);
    }
}