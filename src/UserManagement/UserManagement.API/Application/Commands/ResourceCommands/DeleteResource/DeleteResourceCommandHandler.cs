using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ResourceCommands.DeleteResource;

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, Result<Guid>>
{
    private readonly IResourceRepository _repository;

    public DeleteResourceCommandHandler(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.GetByIdAsync(request.Id);

        _repository.Delete(resource);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}