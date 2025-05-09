using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContractStatus;

public class UpdateServiceContractStatusCommandHandler : IRequestHandler<UpdateServiceContractStatusCommand, Result<FullServiceContractViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public UpdateServiceContractStatusCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }
    public async Task<Result<FullServiceContractViewModel>> Handle(UpdateServiceContractStatusCommand request, CancellationToken cancellationToken)
    {
        var serviceContract = await _serviceContractRepository.GetByIdAsync(request.UpdateServiceContractStatusRequest.Id);
        var serviceContractStatus = await _serviceContractRepository.GetServiceContractStatusByIdAsync(request.UpdateServiceContractStatusRequest.ServiceContractStatusId);
        var serviceContractStatusReason = await _serviceContractRepository.GetServiceContractStatusReasonByIdAsync(request.UpdateServiceContractStatusRequest.ServiceContractStatusReasonId);
        
        serviceContract.AddServiceContractStatusHistory(serviceContractStatus, serviceContractStatusReason);

        _serviceContractRepository.Update(serviceContract);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var serviceContractDto = _mapper.Map<FullServiceContractViewModel>(serviceContract);

        return Result<FullServiceContractViewModel>.SuccessResult(serviceContractDto, "ServiceContractStatus updated successfully.");
    }
}
