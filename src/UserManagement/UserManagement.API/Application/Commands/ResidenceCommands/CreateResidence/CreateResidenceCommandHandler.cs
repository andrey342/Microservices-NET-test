using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;
public class CreateResidenceCommandHandler : IRequestHandler<CreateResidenceCommand, Result<ResidenceViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public CreateResidenceCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<ResidenceViewModel>> Handle(CreateResidenceCommand request, CancellationToken cancellationToken)
    {
        var serviceContract = await _serviceContractRepository.GetByIdAsync(request.AddResidenceToServiceContractRequest.ServiceContractId);
        if (serviceContract == null)
        {
            return Result<ResidenceViewModel>.FailureResult("ServiceContract not found.");
        }

        var residence = _mapper.Map<Residence>(request.AddResidenceToServiceContractRequest);
        serviceContract.AddResidence(residence);

        _serviceContractRepository.Update(serviceContract);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var residenceViewModel = _mapper.Map<ResidenceViewModel>(residence);

        return Result<ResidenceViewModel>.SuccessResult(residenceViewModel, "Add residence to serviceContract successfully.");
    }
}