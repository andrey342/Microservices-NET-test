using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;

public class CreateWorkCenterResourceCommandHandler : IRequestHandler<CreateWorkCenterResourceCommand, Result<Guid>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CreateWorkCenterResourceCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateWorkCenterResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = new WorkCenterResource(_mapper.Map<WorkCenterResource>(request.Request));
        _repository.AddWorkCenterResource(entity);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(entity.Id, "WorkCenterResource created successfully.");
    }
}