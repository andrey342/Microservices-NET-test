using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateCurrentResidence;

public class UpdateCurrentResidenceCommandHandler : IRequestHandler<UpdateCurrentResidenceCommand, Result<FullServiceContractViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public UpdateCurrentResidenceCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }
    public async Task<Result<FullServiceContractViewModel>> Handle(UpdateCurrentResidenceCommand request, CancellationToken cancellationToken)
    {
        var serviceContract = await _serviceContractRepository.GetByIdAsync(request.ActiveResidenceRequest.Id);

        serviceContract.SetCurrentResidence(request.ActiveResidenceRequest.ResidenceId);

        _serviceContractRepository.Update(serviceContract);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var serviceContractDto = _mapper.Map<FullServiceContractViewModel>(serviceContract);

        return Result<FullServiceContractViewModel>.SuccessResult(serviceContractDto, "Current residence updated successfully.");
    }
}
