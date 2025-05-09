using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AreaLevelCommands.DeleteAreaLevel;

public class DeleteAreaLevelCommandHandler : IRequestHandler<DeleteAreaLevelCommand, Result<Guid>>
{
    private readonly IAreaLevelRepository _repository;

    public DeleteAreaLevelCommandHandler(IAreaLevelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(DeleteAreaLevelCommand request, CancellationToken cancellationToken)
    {
        var areaLevel = await _repository.GetByIdAsync(request.Id);

        _repository.Delete(areaLevel);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}
