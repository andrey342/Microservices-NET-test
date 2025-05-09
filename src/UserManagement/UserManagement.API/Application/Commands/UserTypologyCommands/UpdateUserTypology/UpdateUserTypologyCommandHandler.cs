using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserTypologyCommands.UpdateUserTypology;

public class UpdateUserTypologyCommandHandler : IRequestHandler<UpdateUserTypologyCommand, Result<Guid>>
{
    private readonly IUserTypologyRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserTypologyCommandHandler(IUserTypologyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateUserTypologyCommand request, CancellationToken cancellationToken)
    {
        var userTypology = await _repository.GetByIdAsync(request.Id);

        _repository.Update(_mapper.Map(request, userTypology));

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(userTypology.Id, "UserTypology updated successfully.");
    }
}
