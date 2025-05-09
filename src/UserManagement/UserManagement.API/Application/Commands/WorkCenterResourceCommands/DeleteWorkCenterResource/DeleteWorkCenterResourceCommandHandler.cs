using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.DeleteWorkCenterResource;

public class DeleteWorkCenterResourceCommandHandler : IRequestHandler<DeleteWorkCenterResourceCommand, Result<Unit>>
{
    private readonly IUserRepository _repository;

    public DeleteWorkCenterResourceCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(DeleteWorkCenterResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetWorkCenterResourceByIdAsync(request.Id);
        if (entity == null) return Result<Unit>.FailureResult("WorkCenterResource not found.");

        _repository.DeleteWorkCenterResource(entity);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "WorkCenterResource deleted successfully.");
    }
}
