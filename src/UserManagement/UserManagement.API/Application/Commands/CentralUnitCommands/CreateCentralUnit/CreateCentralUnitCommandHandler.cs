using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;

public class CreateCentralUnitCommandHandler : IRequestHandler<CreateCentralUnitCommand, Result<Guid>>
{
    private readonly ICentralUnitRepository _centralUnitRepository;
    private readonly IMapper _mapper;
    public CreateCentralUnitCommandHandler(ICentralUnitRepository centralUnitRepository, IMapper mapper)
    {
        _centralUnitRepository = centralUnitRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateCentralUnitCommand request, CancellationToken cancellationToken)
    {
        var centralUnit = new CentralUnit(_mapper.Map<CentralUnit>(request.CentralUnitRequest));

        _centralUnitRepository.Add(centralUnit);

        await _centralUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(centralUnit.Id, "CentralUnit created successfully.");
    }
}