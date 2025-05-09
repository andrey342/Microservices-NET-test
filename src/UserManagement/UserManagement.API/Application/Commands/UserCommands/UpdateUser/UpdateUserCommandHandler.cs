using AutoMapper;
using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<BasicUserViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<BasicUserViewModel>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(request.UserRequest.Id);

        existingUser.Update(_mapper.Map<User>(request.UserRequest));

        // Verificar si la identification existe
        var identification = await _userRepository.GetIdentificationByNumberAsync(request.UserRequest.Identification.Number);

        if (identification == null) // SI NO EXISTE
        {
            identification = new Identification(_mapper.Map<Identification>(request.UserRequest.Identification));
        }
        else // SI existe
        {
            var valuesIdentificacion = request.UserRequest.Identification;
            identification.UpdateTypeAndDates(valuesIdentificacion.TypeId, valuesIdentificacion.ExpirationDate, valuesIdentificacion.UpdateDate);
        }

        existingUser.AddIdentification(identification);

        // Verificar y asignar PreferredProfessional
        if (request.UserRequest.PreferredProfessional != null)
        {
            var preferredProfessional = await _userRepository.GetPreferredProfessionalByProfessionalId(request.UserRequest.PreferredProfessional.ProfessionalId);
            if (preferredProfessional == null)
            {
                preferredProfessional = new PreferredProfessional(_mapper.Map<PreferredProfessional>(request.UserRequest.PreferredProfessional));
            }
            existingUser.AssignPreferredProfessional(preferredProfessional);
        }
        else
        {
            existingUser.RemovePreferredProfessional();
        }

        _userRepository.Update(existingUser);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var userViewModel = _mapper.Map<BasicUserViewModel>(existingUser);

        return Result<BasicUserViewModel>.SuccessResult(userViewModel, "User updated successfully.");
    }
}
