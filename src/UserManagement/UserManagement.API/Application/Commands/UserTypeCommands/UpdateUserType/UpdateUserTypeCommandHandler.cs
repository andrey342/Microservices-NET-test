using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserTypeCommands.UpdateUserType
{
    public class UpdateUserTypeCommandHandler : IRequestHandler<UpdateUserTypeCommand, Result<Guid>>
    {
        private readonly IUserTypeRepository _repository;
        private readonly IMapper _mapper;

        public UpdateUserTypeCommandHandler(IUserTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UpdateUserTypeCommand request, CancellationToken cancellationToken)
        {
            var userType = await _repository.GetByIdAsync(request.Id);

            _repository.Update(_mapper.Map(request, userType));

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Result<Guid>.SuccessResult(userType.Id, "UserType updated successfully.");
        }
    }
}
