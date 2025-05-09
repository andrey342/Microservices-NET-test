using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ResourceCommands.UpdateResource;

public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, Result<Guid>>
{
    private readonly IResourceRepository _repository;
    private readonly IMapper _mapper;

    public UpdateResourceCommandHandler(IResourceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.GetByIdAsync(request.Id);

        _repository.Update(_mapper.Map(request, resource));

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(resource.Id, "Resource updated successfully.");
    }
}