using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AreaCommands.DeleteArea;

public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, Result<Guid>>
{
    private readonly IAreaRepository _areaRepository;

    public DeleteAreaCommandHandler(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
    {
        var area = await _areaRepository.GetByIdAsync(request.Id);

        _areaRepository.Delete(area);

        await _areaRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}
