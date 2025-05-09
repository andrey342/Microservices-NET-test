using UserManagement.Domain.AggregateModel.WorkCenterAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AreaCommands.CreateArea;

public class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, Result<Guid>>
{
    private readonly IAreaRepository _areaRepository;
    private readonly IMapper _mapper;

    public CreateAreaCommandHandler(IAreaRepository areaRepository, IMapper mapper)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
    {
        var area = new Area(_mapper.Map<Area>(request));

        _areaRepository.Add(area);

        await _areaRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(area.Id, "Area created successfully.");
    }
}