using UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddPeripheral;

public class AddPeripheralCommandHandler : IRequestHandler<AddPeripheralCommand, Result<Guid>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IPeripheralRepository _peripheralRepository; 
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddPeripheralCommandHandler(IServiceContractRepository serviceContractRepository, IPeripheralRepository peripheralRepository, IMapper mapper, IMediator mediator)
    {
        _serviceContractRepository = serviceContractRepository;
        _peripheralRepository = peripheralRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<Guid>> Handle(AddPeripheralCommand request, CancellationToken cancellationToken)
    {
        var serviceContractCentralUnit = await _serviceContractRepository.GetServiceContractCentralUnitByIdAsync(request.PeripheralRequest.ServiceContractCentralUnitId);

        Peripheral peripheral;

        if (request.PeripheralRequest.PeripheralId.HasValue)
        {
            // Case 1: Existing central unit
            peripheral = await _peripheralRepository.GetByIdAsync(request.PeripheralRequest.PeripheralId.Value);
            if (peripheral == null)
            {
                return Result<Guid>.FailureResult("Peripheral not found.");
            }
        }
        else if (request.PeripheralRequest.Peripheral != null)
        {
            //Case 2: Create central unit first
            var createPeripheralCommand = new CreatePeripheralCommand(request.PeripheralRequest.Peripheral);

            var createPeripheralResult = await _mediator.Send(createPeripheralCommand, cancellationToken);
            if (!createPeripheralResult.Success)
            {
                return Result<Guid>.FailureResult("Error creating peripheral.");
            }

            peripheral = await _peripheralRepository.GetByIdAsync(createPeripheralResult.Data);
        }
        else
        {
            return Result<Guid>.FailureResult("Either PeripheralId or PeripheralRequest must be provided.");
        }

        serviceContractCentralUnit.AddPeripheral(peripheral);
        
        _serviceContractRepository.UpdateServiceContractCentralUnit(serviceContractCentralUnit);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(serviceContractCentralUnit.Id);
    }
}