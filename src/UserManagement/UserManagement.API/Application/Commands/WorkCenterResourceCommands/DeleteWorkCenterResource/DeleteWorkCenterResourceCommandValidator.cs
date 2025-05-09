using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.DeleteWorkCenterResource;

public class DeleteWorkCenterResourceCommandValidator : BaseValidator<DeleteWorkCenterResourceCommand>
{
    public DeleteWorkCenterResourceCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<WorkCenterResource>(x => x.Id, isRequired: true);
    }
}
