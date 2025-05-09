using UserManagement.API.Application.Common.Models;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.CohabitantCommands.DeleteCohabitant;

public class DeleteCohabitantCommandHandler : IRequestHandler<DeleteCohabitantCommand, Result<Unit>>
{
    private readonly IServiceContractRepository _serviceContractRepository;

    public DeleteCohabitantCommandHandler(IServiceContractRepository serviceContractRepository)
    {
        _serviceContractRepository = serviceContractRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteCohabitantCommand request, CancellationToken cancellationToken)
    {
        var cohabitant = await _serviceContractRepository.GetCohabitantByIdAsync(request.Id);
        var residence = await _serviceContractRepository.GetResidenceByIdAsync(cohabitant.ResidenceId);

        residence.RemoveCohabitant(request.Id);
        _serviceContractRepository.DeleteCohabitant(cohabitant);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Result<Unit>.SuccessResult(Unit.Value, "Cohabitant deleted successfully.");
    }
}