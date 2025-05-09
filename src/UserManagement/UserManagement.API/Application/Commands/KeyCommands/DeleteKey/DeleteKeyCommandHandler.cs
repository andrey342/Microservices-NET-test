using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.KeyCommands.DeleteKey
{
    public class DeleteKeyCommandHandler : IRequestHandler<DeleteKeyCommand, Result<Unit>>
    {
        private readonly IServiceContractRepository _serviceContractRepository;
        public DeleteKeyCommandHandler(IServiceContractRepository serviceContractRepository) {

            _serviceContractRepository = serviceContractRepository;
        }
        public async Task<Result<Unit>> Handle(DeleteKeyCommand request, CancellationToken cancellationToken)
        {
            var key = await _serviceContractRepository.GetKeyByIdAsync(request.Id);
            var residence = await _serviceContractRepository.GetResidenceByIdAsync(key.ResidenceId);

            residence.RemoveKey(request.Id);
            _serviceContractRepository.DeleteKey(key);

            await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Result<Unit>.SuccessResult(Unit.Value, "Key deleted successfully.");
        }
    }
}
