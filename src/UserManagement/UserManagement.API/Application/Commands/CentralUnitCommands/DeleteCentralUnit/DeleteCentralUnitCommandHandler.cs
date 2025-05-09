using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.DeleteCentralUnit;

public class DeleteCentralUnitCommandHandler : IRequestHandler<DeleteCentralUnitCommand, Result<Guid>>
{
    private readonly ICentralUnitRepository _repo;

    public DeleteCentralUnitCommandHandler(ICentralUnitRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<Guid>> Handle(DeleteCentralUnitCommand request, CancellationToken cancellationToken)
    {
        var centralUnit = await _repo.GetByIdAsync(request.Id);

        _repo.Delete(centralUnit);

        await _repo.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}

