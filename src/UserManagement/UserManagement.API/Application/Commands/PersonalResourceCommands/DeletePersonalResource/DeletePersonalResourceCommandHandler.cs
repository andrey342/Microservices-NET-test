using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PersonalResourceCommands.DeletePersonalResource;

public class DeletePersonalResourceCommandHandler : IRequestHandler<DeletePersonalResourceCommand, Result<Unit>>
{
    private readonly IUserRepository _repository;

    public DeletePersonalResourceCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(DeletePersonalResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetPersonalResourceByIdAsync(request.Id);
        _repository.DeletePersonalResource(entity);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Result<Unit>.SuccessResult(Unit.Value, "PersonalResource eliminado correctamente.");
    }
}