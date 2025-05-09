using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PeripheralCommands.DeletePeripheral;

public class DeletePeripheralCommandHandler : IRequestHandler<DeletePeripheralCommand, Result<Guid>>
{
    private readonly IPeripheralRepository _repo;

    public DeletePeripheralCommandHandler(IPeripheralRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<Guid>> Handle(DeletePeripheralCommand request, CancellationToken cancellationToken)
    {
        var item = await _repo.GetByIdAsync(request.Id);

        _repo.Delete(item);

        await _repo.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}

