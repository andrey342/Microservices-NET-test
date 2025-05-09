using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemoveBeneficiary;

public class RemoveBeneficiaryCommandHandler : IRequestHandler<RemoveBeneficiaryCommand, Result<Unit>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    public RemoveBeneficiaryCommandHandler(IServiceContractRepository serviceContractRepository)
    {
        _serviceContractRepository = serviceContractRepository;
    }
    public async Task<Result<Unit>> Handle(RemoveBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var serviceContractBeneficiary = await _serviceContractRepository.GetServiceContractBeneficiaryByIdAsync(request.Id);

        var serviceContract = await _serviceContractRepository.GetByIdAsync(serviceContractBeneficiary.ServiceContractId);
        if (serviceContract == null)
        {
            return Result<Unit>.FailureResult("Service contract not found.");
        }

        serviceContract.RemoveBeneficiary(serviceContractBeneficiary.Id);
        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Beneficiary removed successfully.");
    }
}
