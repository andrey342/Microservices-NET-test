using UserManagement.API.Application.Queries.CentralUnitQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;

public class UpdateCentralUnitCommandHandler : IRequestHandler<UpdateCentralUnitCommand, Result<CentralUnitViewModel>>
{
    private readonly ICentralUnitRepository _centralUnitRepository;
    private readonly IMapper _mapper;

    public UpdateCentralUnitCommandHandler(ICentralUnitRepository centralUnitRepository, IMapper mapper)
    {
        _centralUnitRepository = centralUnitRepository;
        _mapper = mapper;
    }

    public async Task<Result<CentralUnitViewModel>> Handle(UpdateCentralUnitCommand request, CancellationToken cancellationToken)
    {
        var existingCentralUnit = await _centralUnitRepository.GetByIdAsync(request.CentralUnitRequest.Id);

        _mapper.Map(request.CentralUnitRequest, existingCentralUnit);

        _centralUnitRepository.Update(existingCentralUnit);

        await _centralUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var centralUnitViewModel = _mapper.Map<CentralUnitViewModel>(existingCentralUnit);

        return Result<CentralUnitViewModel>.SuccessResult(centralUnitViewModel, "Central unit updated successfully.");
    }
}