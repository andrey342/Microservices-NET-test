using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.ResourceCommands.DeleteResource;

public class DeleteResourceCommandValidator : BaseValidator<DeleteResourceCommand>
{
    public DeleteResourceCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<Resource>(x => x.Id, true);
    }
}