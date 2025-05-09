using AutoMapper;
using UserManagement.API.Application.Commands.WorkCenterCommands.UpdateWorkCenter;
using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

public class UpdateWorkCenterCommandHandler : IRequestHandler<UpdateWorkCenterCommand, Result<WorkCenterUmViewModel>>
{
    private readonly IWorkCenterRepository _workCenterRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateWorkCenterCommandHandler> _logger;

    public UpdateWorkCenterCommandHandler(IWorkCenterRepository workCenterRepository, IMapper mapper, ILogger<UpdateWorkCenterCommandHandler> logger)
    {
        _workCenterRepository = workCenterRepository;
        _mapper = mapper;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<WorkCenterUmViewModel>> Handle(UpdateWorkCenterCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await _workCenterRepository.GetByIdAsync(request.Id);

        if (workCenter == null)
            return Result<WorkCenterUmViewModel>.FailureResult("WorkCenter not found.");

        _mapper.Map(request, workCenter);

        _workCenterRepository.Update(workCenter);

        _logger.LogInformation("Updating WorkCenter - WorkCenter: {@WorkCenter}", workCenter);

        await _workCenterRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var workCenterDto = _mapper.Map<WorkCenterUmViewModel>(workCenter);

        return Result<WorkCenterUmViewModel>.SuccessResult(workCenterDto, "WorkCenter updated successfully.");
    }
}