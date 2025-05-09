using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;

public class CreatePeripheralCommandHandler : IRequestHandler<CreatePeripheralCommand, Result<Guid>>
{
    private readonly IPeripheralRepository _peripheralRepository;
    private readonly IMapper _mapper;
    public CreatePeripheralCommandHandler(IPeripheralRepository peripheralRepository, IMapper mapper)
    {
        _peripheralRepository = peripheralRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreatePeripheralCommand request, CancellationToken cancellationToken)
    {
        var peripheral = new Peripheral(_mapper.Map<Peripheral>(request.PeripheralRequest));

        _peripheralRepository.Add(peripheral);

        await _peripheralRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(peripheral.Id, "Peripheral created successfully.");
    }
}