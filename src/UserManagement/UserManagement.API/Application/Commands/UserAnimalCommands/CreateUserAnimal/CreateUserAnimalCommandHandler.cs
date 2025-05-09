using AutoMapper;
using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserAnimalCommands.CreateUserAnimal;

public class CreateUserAnimalCommandHandler : IRequestHandler<CreateUserAnimalCommand, Result<FullUserAnimalViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserAnimalCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<FullUserAnimalViewModel>> Handle(CreateUserAnimalCommand request, CancellationToken cancellationToken)
    {
        // Obtener el usuario
        var user = await _userRepository.GetByIdAsync(request.UserAnimalCreateDto.UserId);
        if (user == null)
        {
            return Result<FullUserAnimalViewModel>.FailureResult("User not found.");
        }

        // Crear la relación UserAnimal
        var userAnimal = new UserAnimal(user.Id, request.UserAnimalCreateDto.AnimalId, request.UserAnimalCreateDto.Name);

        // Agregar el animal al usuario
        user.AddAnimal(userAnimal);

        // Guardar los cambios
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        // Mapear el usuario actualizado a UserDto
        var userAnimalViewModel = _mapper.Map<FullUserAnimalViewModel>(userAnimal);

        return Result<FullUserAnimalViewModel>.SuccessResult(userAnimalViewModel, "Animal added to user successfully.");
    }
}
