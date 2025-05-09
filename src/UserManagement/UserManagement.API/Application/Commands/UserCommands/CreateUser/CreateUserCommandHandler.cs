using AutoMapper;
using System.Reflection;
using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.IntegrationEvents;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserIntegrationEventService _userIntegrationEventService;
    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUserIntegrationEventService userIntegrationEventService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userIntegrationEventService = userIntegrationEventService ?? throw new ArgumentNullException(nameof(userIntegrationEventService));
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userCreatedIntegrationEvent = new UserCreatedIntegrationEvent(request.UserRequest.Name);
        await _userIntegrationEventService.AddAndSaveEventAsync(userCreatedIntegrationEvent);

        var user = new User(_mapper.Map<User>(request.UserRequest));

        // Verificar si la identification existe
        var identification = await _userRepository.GetIdentificationByNumberAsync(request.UserRequest.Identification.Number);

        if (identification == null)
        {
            identification = new Identification(_mapper.Map<Identification>(request.UserRequest.Identification));
        }

        user.AddIdentification(identification);

        // Verificar si el PreferredProfessional ya existe
        if (request.UserRequest.PreferredProfessional!=null)
        {
            var preferredProfessional = await _userRepository.GetPreferredProfessionalByProfessionalId(request.UserRequest.PreferredProfessional.ProfessionalId);

            // Si no existe, crear uno nuevo
            if (preferredProfessional == null)
            {
                preferredProfessional = new PreferredProfessional(_mapper.Map<PreferredProfessional>(request.UserRequest.PreferredProfessional));
            }

            user.AssignPreferredProfessional(preferredProfessional);
        }

        _userRepository.Add(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var userDto = _mapper.Map<BasicUserViewModel>(user);

        return Result<Guid>.SuccessResult(userDto.Id, "User created successfully.");
    }
}