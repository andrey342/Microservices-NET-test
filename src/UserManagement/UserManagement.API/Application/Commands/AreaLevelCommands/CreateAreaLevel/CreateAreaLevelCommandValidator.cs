using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.AreaLevelCommands.CreateAreaLevel;

public class CreateAreaLevelCommandValidator : BaseValidator<CreateAreaLevelCommand>
{
    public CreateAreaLevelCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<WorkCenter>(x => x.WorkCenterId, isRequired: true);
        ValidateString(x => x.Name, maxLength: 100, isRequired: true);
        ValidatePositiveNumber(x => x.Level, isRequired: true);
    }
}