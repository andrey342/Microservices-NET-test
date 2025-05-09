using UserManagement.API.Application.Queries.PeripheralQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;

public class UpdatePeripheralCommandHandler : IRequestHandler<UpdatePeripheralCommand, Result<PeripheralViewModel>>
{
    private readonly IPeripheralRepository _peripheralRepository;
    private readonly IMapper _mapper;

    public UpdatePeripheralCommandHandler(IPeripheralRepository peripheralRepository, IMapper mapper)
    {
        _peripheralRepository = peripheralRepository;
        _mapper = mapper;
    }

    public async Task<Result<PeripheralViewModel>> Handle(UpdatePeripheralCommand request, CancellationToken cancellationToken)
    {
        var existingPeripheral = await _peripheralRepository.GetByIdAsync(request.PeripheralRequest.Id);

        _mapper.Map(request.PeripheralRequest, existingPeripheral);

        _peripheralRepository.Update(existingPeripheral);

        await _peripheralRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var peripheralViewModel = _mapper.Map<PeripheralViewModel>(existingPeripheral);

        return Result<PeripheralViewModel>.SuccessResult(peripheralViewModel, "Peripheral updated successfully.");
    }
}
