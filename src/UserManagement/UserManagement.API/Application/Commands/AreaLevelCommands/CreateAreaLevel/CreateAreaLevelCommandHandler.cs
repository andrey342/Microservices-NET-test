using UserManagement.Domain.AggregateModel.WorkCenterAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.AreaLevelCommands.CreateAreaLevel;

public class CreateAreaLevelCommandHandler : IRequestHandler<CreateAreaLevelCommand, Result<Guid>>
{
    private readonly IAreaLevelRepository _areaLevelRepository;
    private readonly IMapper _mapper;

    public CreateAreaLevelCommandHandler(IAreaLevelRepository areaLevelRepository, IMapper mapper)
    {
        _areaLevelRepository = areaLevelRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateAreaLevelCommand request, CancellationToken cancellationToken)
    {
        var areaLevel = new AreaLevel(_mapper.Map<AreaLevel>(request));

        _areaLevelRepository.Add(areaLevel);

        await _areaLevelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(areaLevel.Id, "Area level created successfully.");
    }
}