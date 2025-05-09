using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.AreaCommands.UpdateArea;

public class UpdateAreaCommandValidator : BaseValidator<UpdateAreaCommand>
{
    public UpdateAreaCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<Area>(x => x.Id, true);
        ValidateGuid<AreaLevel>(x => x.AreaLevelId, true);
        ValidateGuid<Area>(x => x.ParentId, false);
        ValidateString(x => x.Name, 100, true);
    }
}