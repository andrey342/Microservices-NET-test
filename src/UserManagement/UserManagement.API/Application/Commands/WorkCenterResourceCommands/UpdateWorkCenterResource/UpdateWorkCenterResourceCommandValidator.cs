using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.UpdateWorkCenterResource;

public class UpdateWorkCenterResourceCommandValidator : BaseValidator<UpdateWorkCenterResourceCommand>
{
    public UpdateWorkCenterResourceCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<WorkCenterResource>(x => x.Request.Id, isRequired: true);
        ValidateGuid<User>(x => x.Request.UserId, isRequired: true);
        ValidateGuid<Resource>(x => x.Request.ResourceId, isRequired: true);
        ValidateString(x => x.Request.Observations, 200, false);
    }
}
