using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemovePeripheral;

public class RemovePeripheralCommandHandler : IRequestHandler<RemovePeripheralCommand, Result<Unit>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IPeripheralRepository _peripheralRepository;
    public RemovePeripheralCommandHandler(IServiceContractRepository serviceContractRepository, IPeripheralRepository peripheralRepository)
    {
        _serviceContractRepository = serviceContractRepository;
        _peripheralRepository = peripheralRepository;
    }
    public async Task<Result<Unit>> Handle(RemovePeripheralCommand request, CancellationToken cancellationToken)
    {
        var peripheral = await _peripheralRepository.GetByIdAsync(request.Id);
        if (peripheral == null || !peripheral.ServiceContractCentralUnitId.HasValue)
        {
            return Result<Unit>.FailureResult("Peripheral or Service contract central unit not found.");
        }

        var serviceContractCentralUnit = await _serviceContractRepository.GetServiceContractCentralUnitByIdAsync(peripheral.ServiceContractCentralUnitId.Value);
        if (serviceContractCentralUnit == null)
        {
            return Result<Unit>.FailureResult("Service contract central unit not found.");
        }

        serviceContractCentralUnit.RemovePeripheral(peripheral);
        _serviceContractRepository.UpdateServiceContractCentralUnit(serviceContractCentralUnit);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Peripheral removed successfully.");
    }
}