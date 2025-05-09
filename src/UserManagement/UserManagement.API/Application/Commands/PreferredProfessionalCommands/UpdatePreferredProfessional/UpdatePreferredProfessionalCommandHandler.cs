using AutoMapper;
using UserManagement.API.Application.Common.Models;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PreferredProfessionalCommands.UpdatePreferredProfessional;

public class UpdatePreferredProfessionalCommandHandler : IRequestHandler<UpdatePreferredProfessionalCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdatePreferredProfessionalCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdatePreferredProfessionalCommand request, CancellationToken cancellationToken)
    {

        var preferredProfessional = await _userRepository.GetPreferredProfessionalByProfessionalId(request.ProfessionalId);
        if(preferredProfessional == null)
        {
            return Result<Guid>.FailureResult($"Preferred professional with professional id {request.ProfessionalId} not found.");
        }

        preferredProfessional.Update(request.Name, request.Surname1, request.Surname2);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(preferredProfessional.Id, "Preferred professional updated successfully.");
    }
}
