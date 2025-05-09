using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.KeyCommands.UpdateKey;

public class UpdateKeyCommandHandler : IRequestHandler<UpdateKeyCommand, Result<KeyViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public UpdateKeyCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<KeyViewModel>> Handle(UpdateKeyCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var existingKey = await _serviceContractRepository.GetKeyByIdAsync(request.Id);

        // Comprobar si el estado ha cambiado
        if (existingKey.CurrentStatusId != request.CurrentStatusId)
        {
            var keyStatus = await _serviceContractRepository.GetKeyStatusByIdAsync(request.CurrentStatusId);
            if (keyStatus == null)
            {
                return Result<KeyViewModel>.FailureResult($"Key status with id {request.CurrentStatusId} not found.");
            }
            // Añadir el nuevo estado al historial
            existingKey.AddKeyHistory(keyStatus);
        }

        existingKey.Update(_mapper.Map<Key>(request));

        _serviceContractRepository.UpdateKey(existingKey);
        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var keyViewModel = _mapper.Map<KeyViewModel>(existingKey);

        return Result<KeyViewModel>.SuccessResult(keyViewModel, "Key updated successfully.");
    }
}