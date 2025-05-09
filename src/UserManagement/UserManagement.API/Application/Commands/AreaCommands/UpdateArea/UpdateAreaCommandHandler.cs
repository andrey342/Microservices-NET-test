using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AreaCommands.UpdateArea;

public class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand, Result<Guid>>
{
    private readonly IAreaRepository _repository;
    private readonly IMapper _mapper;

    public UpdateAreaCommandHandler(IAreaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
    {
        var area = await _repository.GetByIdAsync(request.Id);

        _repository.Update(_mapper.Map(request, area));

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(area.Id, "Area updated successfully.");
    }
}

