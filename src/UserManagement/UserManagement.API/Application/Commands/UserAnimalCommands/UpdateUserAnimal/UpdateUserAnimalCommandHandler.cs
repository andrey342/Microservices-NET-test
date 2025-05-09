using AutoMapper;
using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.UpdateUserAnimal;

public class UpdateUserAnimalCommandHandler : IRequestHandler<UpdateUserAnimalCommand, Result<BasicUserViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserAnimalCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<BasicUserViewModel>> Handle(UpdateUserAnimalCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserAnimalUpdate.UserId);
        if (user == null) return Result<BasicUserViewModel>.FailureResult("User not found.");

        user.UpdateAnimal(request.UserAnimalUpdate.AnimalId, request.UserAnimalUpdate.Name);
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var userViewModel = _mapper.Map<BasicUserViewModel>(user);

        return Result<BasicUserViewModel>.SuccessResult(userViewModel, "Animal updated for user.");
    }
}
