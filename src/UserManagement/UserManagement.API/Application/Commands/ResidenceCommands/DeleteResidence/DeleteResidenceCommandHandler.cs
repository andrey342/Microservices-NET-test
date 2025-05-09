using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ResidenceCommands.DeleteResidence;

public class DeleteResidenceCommandHandler : IRequestHandler<DeleteResidenceCommand, Result<Unit>>
{
    private readonly IServiceContractRepository _serviceContractRepository;

    public DeleteResidenceCommandHandler(IServiceContractRepository serviceContractRepository)
    {
        _serviceContractRepository = serviceContractRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteResidenceCommand request, CancellationToken cancellationToken)
    {
        var residence = await _serviceContractRepository.GetResidenceByIdAsync(request.Id);
        var serviceContract = await _serviceContractRepository.GetByIdAsync(residence.ServiceContractId);

        serviceContract.RemoveResidence(request.Id);
        _serviceContractRepository.Update(serviceContract);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Result<Unit>.SuccessResult(Unit.Value, "Residence deleted successfully.");
    }
}