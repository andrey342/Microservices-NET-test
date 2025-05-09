using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Commands.AreaCommands.DeleteArea;

public class DeleteAreaCommandValidator : BaseValidator<DeleteAreaCommand>
{
    public DeleteAreaCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        // Validación de campos obligatorios
        ValidateGuid<Area>(x => x.Id, true);
    }
}