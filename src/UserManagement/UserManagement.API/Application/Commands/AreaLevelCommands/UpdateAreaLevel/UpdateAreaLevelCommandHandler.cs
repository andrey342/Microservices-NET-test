using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AreaLevelCommands.UpdateAreaLevel;

public class UpdateAreaLevelCommandHandler : IRequestHandler<UpdateAreaLevelCommand, Result<Guid>>
{
    private readonly IAreaLevelRepository _repository;
    private readonly IMapper _mapper;

    public UpdateAreaLevelCommandHandler(IAreaLevelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateAreaLevelCommand request, CancellationToken cancellationToken)
    {
        var areaLevel = await _repository.GetByIdAsync(request.Id);

        _repository.Update(_mapper.Map(request, areaLevel));

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(areaLevel.Id, "Area level updated successfully.");
    }
}

