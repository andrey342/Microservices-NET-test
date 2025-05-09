using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.UpdateWorkCenterResource;

public class UpdateWorkCenterResourceCommandHandler : IRequestHandler<UpdateWorkCenterResourceCommand, Result<Unit>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateWorkCenterResourceCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(UpdateWorkCenterResourceCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetWorkCenterResourceByIdAsync(request.Request.Id);
        if (existing == null) return Result<Unit>.FailureResult("WorkCenterResource not found.");

        existing.Update(_mapper.Map(request.Request, existing));
        _repository.UpdateWorkCenterResource(existing);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "WorkCenterResource updated successfully.");
    }
}
