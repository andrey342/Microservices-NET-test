using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserTypologyCommands.CreateUserTypology;

public class CreateUserTypologyCommandHandler : IRequestHandler<CreateUserTypologyCommand, Result<Guid>>
{
    private readonly IUserTypologyRepository _repository;
    private readonly IMapper _mapper;

    public CreateUserTypologyCommandHandler(IUserTypologyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateUserTypologyCommand request, CancellationToken cancellationToken)
    {
        var userTypology = new UserTypology(_mapper.Map<UserTypology>(request));

        _repository.Add(userTypology);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(userTypology.Id, "UserTypology created successfully.");
    }
}