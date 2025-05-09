using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Migrations;

namespace UserManagement.API.Application.Commands.KeyCommands.CreateKey;

public class CreateKeyCommandHandler : IRequestHandler<CreateKeyCommand, Result<Guid>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public CreateKeyCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateKeyCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var residence = await _serviceContractRepository.GetResidenceByIdAsync(request.ResidenceId);

        var key = new Key(_mapper.Map<Key>(request));

        // Add default key status to the serviceContract
        var keyStatus = await _serviceContractRepository.GetDefaultKeyStatusAsync();

        if (keyStatus != null)
        {
            key.AddKeyHistory(keyStatus);
        }

        residence.AddKey(key);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(key.Id, "Key added successfully.");
    }
}