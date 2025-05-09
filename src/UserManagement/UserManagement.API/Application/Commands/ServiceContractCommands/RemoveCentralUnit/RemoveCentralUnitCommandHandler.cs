using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemoveCentralUnit
{
    public class RemoveCentralUnitCommandHandler : IRequestHandler<RemoveCentralUnitCommand, Result<Unit>>
    {
        private readonly IServiceContractRepository _serviceContractRepository;
        public RemoveCentralUnitCommandHandler(IServiceContractRepository serviceContractRepository)
        {
            _serviceContractRepository = serviceContractRepository;
        }
        public async Task<Result<Unit>> Handle(RemoveCentralUnitCommand request, CancellationToken cancellationToken)
        {
            var serviceContractCentralUnit = await _serviceContractRepository.GetServiceContractCentralUnitByIdAsync(request.Id);

            var serviceContract = await _serviceContractRepository.GetByIdAsync(serviceContractCentralUnit.ServiceContractId);
            if(serviceContract == null)
            {
                return Result<Unit>.FailureResult("Service contract not found.");
            }

            serviceContract.RemoveServiceContractCentralUnit(serviceContractCentralUnit.Id);
            await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Result<Unit>.SuccessResult(Unit.Value, "Central unit removed successfully.");
        }
    }
}
