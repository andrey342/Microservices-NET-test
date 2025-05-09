using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserTypeCommands.CreateUserType;

public class CreateUserTypeCommandHandler : IRequestHandler<CreateUserTypeCommand, Result<Guid>>
{
    private readonly IUserTypeRepository _userTypeRepository;
    private readonly IMapper _mapper;

    public CreateUserTypeCommandHandler(IUserTypeRepository userTypeRepository, IMapper mapper)
    {
        _userTypeRepository = userTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        var userType = new UserType(_mapper.Map<UserType>(request));

        _userTypeRepository.Add(userType);

        await _userTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(userType.Id, "UserType created successfully.");
    }
}