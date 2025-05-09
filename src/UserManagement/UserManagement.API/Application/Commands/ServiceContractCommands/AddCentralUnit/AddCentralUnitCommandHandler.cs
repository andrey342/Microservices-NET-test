using UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddCentralUnit;

public class AddCentralUnitToServiceContractCommandHandler : IRequestHandler<AddCentralUnitCommand, Result<Guid>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly ICentralUnitRepository _centralUnitRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddCentralUnitToServiceContractCommandHandler(IServiceContractRepository serviceContractRepository, ICentralUnitRepository centralUnitRepository, IMapper mapper, IMediator mediator)
    {
        _serviceContractRepository = serviceContractRepository;
        _centralUnitRepository = centralUnitRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<Guid>> Handle(AddCentralUnitCommand request, CancellationToken cancellationToken)
    {
        if (!request.CentralUnitRequest.CentralUnitId.HasValue && request.CentralUnitRequest.CentralUnit == null)
        {
            return Result<Guid>.FailureResult("Either CentralUnitId or CentralUnit must be provided.");
        }

        var serviceContract = await _serviceContractRepository.GetByIdAsync(request.CentralUnitRequest.ServiceContractId);

        var serviceContractCentralUnit = _mapper.Map<ServiceContractCentralUnit>(request.CentralUnitRequest);
        serviceContract.AddServiceContractCentralUnit(serviceContractCentralUnit);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(serviceContractCentralUnit.Id);
    }
}
