using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;

public class CreatePersonalResourceCommandHandler : IRequestHandler<CreatePersonalResourceCommand, Result<Guid>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CreatePersonalResourceCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreatePersonalResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = new PersonalResource(_mapper.Map<PersonalResource>(request.Request));
        _repository.AddPersonalResource(entity);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(entity.Id, "PersonalResource creado correctamente.");
    }
}
