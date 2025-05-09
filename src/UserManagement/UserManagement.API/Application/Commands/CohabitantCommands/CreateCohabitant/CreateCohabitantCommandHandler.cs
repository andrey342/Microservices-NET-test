using AutoMapper;
using UserManagement.API.Application.Common.Models;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;

public class CreateCohabitantCommandHandler : IRequestHandler<CreateCohabitantCommand, Result<Guid>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public CreateCohabitantCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateCohabitantCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var residence = await _serviceContractRepository.GetResidenceByIdAsync(request.ResidenceId);

        var cohabitant = new Cohabitant(_mapper.Map<Cohabitant>(request));
        residence.AddCohabitant(cohabitant);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        if (cohabitant.Resource)
        {
            // Crear recurso personal del usuario
        }

        return Result<Guid>.SuccessResult(cohabitant.Id, "Cohabitant added successfully.");
    }
}