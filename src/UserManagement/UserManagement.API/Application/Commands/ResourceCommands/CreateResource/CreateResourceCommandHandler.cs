using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ResourceCommands.CreateResource;

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Result<Guid>>
{
    private readonly IResourceRepository _repository;
    private readonly IMapper _mapper;

    public CreateResourceCommandHandler(IResourceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = new Resource(_mapper.Map<Resource>(request));

        _repository.Add(resource);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(resource.Id, "Resource created successfully.");
    }
}