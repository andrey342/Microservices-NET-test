using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.AreaCommands.CreateArea;

public class CreateAreaCommandValidator : BaseValidator<CreateAreaCommand>
{
    public CreateAreaCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<AreaLevel>(x => x.AreaLevelId, isRequired: true);
        ValidateString(x => x.Name, maxLength: 100, isRequired: true);
        ValidateGuid<Area>(x => x.ParentId, isRequired: false);
    }
}