using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PersonalResourceCommands.UpdatePersonalResource;

public class UpdatePersonalResourceCommandHandler : IRequestHandler<UpdatePersonalResourceCommand, Result<Unit>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePersonalResourceCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(UpdatePersonalResourceCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetPersonalResourceByIdAsync(request.Request.Id);
        existing.Update(_mapper.Map(request.Request, existing));

        _repository.UpdatePersonalResource(existing);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "PersonalResource actualizado correctamente.");
    }
}
