using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.WorkCenterCommands.CreateWorkCenter;

public class CreateWorkCenterCommandHandler : IRequestHandler<CreateWorkCenterCommand, Result<WorkCenterUmViewModel>>
{
    private readonly IWorkCenterRepository _workCenterRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateWorkCenterCommandHandler> _logger;

    public CreateWorkCenterCommandHandler(IWorkCenterRepository workCenterRepository, IMapper mapper, ILogger<CreateWorkCenterCommandHandler> logger)
    {
        _workCenterRepository = workCenterRepository;
        _mapper = mapper;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<WorkCenterUmViewModel>> Handle(CreateWorkCenterCommand request, CancellationToken cancellationToken)
    {
        var workCenter = new WorkCenter(
            request.Id,
            request.Name
        );
        _workCenterRepository.Add(workCenter);

        _logger.LogInformation("Creating WorkCenter - WorkCenter: {@WorkCenter}", workCenter);

        await _workCenterRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var workCenterDto = _mapper.Map<WorkCenterUmViewModel>(workCenter);

        return Result<WorkCenterUmViewModel>.SuccessResult(workCenterDto, "User created successfully.");
    }
}