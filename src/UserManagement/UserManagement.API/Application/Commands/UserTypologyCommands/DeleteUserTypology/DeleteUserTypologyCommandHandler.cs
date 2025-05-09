using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserTypologyCommands.DeleteUserTypology;

public class DeleteUserTypologyCommandHandler : IRequestHandler<DeleteUserTypologyCommand, Result<Guid>>
{
    private readonly IUserTypologyRepository _repository;

    public DeleteUserTypologyCommandHandler(IUserTypologyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(DeleteUserTypologyCommand request, CancellationToken cancellationToken)
    {
        var userTypology = await _repository.GetByIdAsync(request.Id);

        _repository.Delete(userTypology);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(request.Id);
    }
}