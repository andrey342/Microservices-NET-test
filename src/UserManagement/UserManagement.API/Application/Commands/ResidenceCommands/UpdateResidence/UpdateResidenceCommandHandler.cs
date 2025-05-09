using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Commands.ResidenceCommands.UpdateResidence;

public class UpdateResidenceCommandHandler : IRequestHandler<UpdateResidenceCommand, Result<ResidenceViewModel>>
{
    private readonly IServiceContractRepository _serviceContractRepository;
    private readonly IMapper _mapper;

    public UpdateResidenceCommandHandler(IServiceContractRepository serviceContractRepository, IMapper mapper)
    {
        _serviceContractRepository = serviceContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<ResidenceViewModel>> Handle(UpdateResidenceCommand request, CancellationToken cancellationToken)
    {
        var serviceContract = await _serviceContractRepository.GetByIdAsync(request.UpdateResidenceInServiceContractRequest.ServiceContractId);
        if (serviceContract == null)
        {
            return Result<ResidenceViewModel>.FailureResult("ServiceContract not found.");
        }

        var residence = serviceContract.Residences.FirstOrDefault(r => r.Id == request.UpdateResidenceInServiceContractRequest.Id);
        if (residence == null)
        {
            return Result<ResidenceViewModel>.FailureResult("Residence not found.");
        }

        // Guarda el valor original de IsCurrentResidence
        var originalIsCurrentResidence = residence.IsCurrentResidence;

        // Mapea las propiedades de la solicitud a la entidad
        _mapper.Map(request.UpdateResidenceInServiceContractRequest, residence);

        // Comprueba si IsCurrentResidence ha sido modificado
        if (originalIsCurrentResidence != residence.IsCurrentResidence && residence.IsCurrentResidence)
        {
            // Desmarca cualquier otra residencia que esté actualmente marcada como IsCurrentResidence
            foreach (var res in serviceContract.Residences)
            {
                res.setCurrentResidence(false);    
            }

            // Guardar cambios para asegurar que no haya residencias marcadas como IsCurrentResidence
            _serviceContractRepository.Update(serviceContract);
            await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            // Marcar la residencia actual como IsCurrentResidence
            serviceContract.SetCurrentResidence(residence.Id);
        }

        _serviceContractRepository.Update(serviceContract);

        await _serviceContractRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var residenceViewModel = _mapper.Map<ResidenceViewModel>(residence);

        return Result<ResidenceViewModel>.SuccessResult(residenceViewModel, "Residence updated successfully.");
    }
}