using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;

public class CreateWorkCenterResourceCommandValidator : BaseValidator<CreateWorkCenterResourceCommand>
{
    public CreateWorkCenterResourceCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<User>(x => x.Request.UserId, isRequired: true);
        ValidateGuid<Resource>(x => x.Request.ResourceId, isRequired: true);
        ValidateString(x => x.Request.Observations, 200, false);
    }
}