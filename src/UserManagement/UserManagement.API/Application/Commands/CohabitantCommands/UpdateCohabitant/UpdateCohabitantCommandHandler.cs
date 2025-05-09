using AutoMapper;
using UserManagement.API.Application.Common.Models;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.CohabitantCommands.UpdateCohabitant;

public class UpdateCohabitantCommandHandler : IRequestHandler<UpdateCohabitantCommand, Result<CohabitantViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public UpdateCohabitantCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<CohabitantViewModel>> Handle(UpdateCohabitantCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var existingCohabitant = await _serviceContractRepository.GetCohabitantByIdAsync(request.Id);

        existingCohabitant.Update(_mapper.Map<Cohabitant>(request));

        _serviceContractRepository.UpdateCohabitant(existingCohabitant);
        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var cohabitantViewModel = _mapper.Map<CohabitantViewModel>(existingCohabitant);

        return Result<CohabitantViewModel>.SuccessResult(cohabitantViewModel, "Cohabitant updated successfully.");
    }
}