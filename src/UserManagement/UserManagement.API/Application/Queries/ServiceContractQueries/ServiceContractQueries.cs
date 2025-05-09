using AutoMapper.QueryableExtensions;
using AutoMapper;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.API.Application.Queries.CentralUnitQueries;
using UserManagement.API.Application.Queries.PeripheralQueries;

namespace UserManagement.API.Application.Queries.ServiceContractQueries;

public class ServiceContractQueries(UserContext context, IMapper mapper, IUserQueries userQueries)
: IServiceContractQueries
{
    /// <summary>
    /// Obtiene un contrato por su ID.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato.</param>
    /// <returns>El contrato correspondiente al ID proporcionado.</returns>
    public async Task<FullServiceContractViewModel> GetServiceContractByIdAsync(Guid serviceContractId)
    {
        var serviceContract = await context.ServiceContract
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Include(c => c.CurrentStatus)
            .Include(c => c.ServiceType)
            .Include(c => c.Residences)
            .Include(c => c.ServiceContractCentralUnits)
                .ThenInclude(cu => cu.Peripherals)
            .Where(c => c.Id == serviceContractId)
            .ProjectTo<FullServiceContractViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException();

        var user = await userQueries.GetUserAsync(serviceContract.UserId);

        return serviceContract with { User = user };
    }

    /// <summary>
    /// Obtiene todos los contratos de un centro de trabajo específico.
    /// </summary>
    /// <param name="workCenterId">ID del centro de trabajo.</param>
    /// <returns>Una lista de contratos correspondientes al centro de trabajo proporcionado.</returns>
    public async Task<IEnumerable<FullServiceContractViewModel>> GetServiceContractsByWorkCenterAsync(Guid workCenterId)
    {
        return await context.ServiceContract
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(c => c.WorkCenterId == workCenterId)
            .ProjectTo<FullServiceContractViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<BasicUserWithContractsViewModel>> GetAllUsersContractByWorkcenterIdAsync(Guid workCenterId)
    {
        // 1) Consulta para obtener el user y el contrato en un solo objeto
        var resultRows = await context.ServiceContract
            .AsNoTracking()
            .Where(sc => sc.WorkCenterId == workCenterId)
            .Include(sc => sc.User)
            .ProjectTo<BasicUserContractObjectsViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        if (!resultRows.Any()) return Enumerable.Empty<BasicUserWithContractsViewModel>();

        // 2) Agrupar por user.Id para evitar duplicados
        var grouped = resultRows
            .GroupBy(row => row.User.Id)
            .Select(group =>
            {
                var first = group.First();
                // user (sin duplicar)
                var userViewModel = mapper.Map<BasicUserWithContractsViewModel>(first.User);

                // 3) Generar la cadena contractStatuses
                var contractStatusNames = group
                    .Select(x => x.ContractStatusName ?? "Desconocido")
                    .Distinct()
                    .ToList();

                userViewModel.ContractStatuses = string.Join(", ", contractStatusNames);

                // 4) Generar la cadena serviceStatuses
                // "serviceTypeName.currentStatusName" 
                var serviceStatuses = group
                    .Select(x => (x.ServiceTypeName ?? "Desconocido") + ": " + (x.ContractStatusName ?? "Desconocido"))
                    .Distinct()
                    .ToList();

                // NUEVO: Lista de IDs de los contratos
                userViewModel.Contracts = group.Select(x => x.Id).ToList();

                userViewModel.ServiceStatuses = string.Join(", ", serviceStatuses);

                return userViewModel;
            })
            .ToList();

        return grouped;
    }

    public async Task<IEnumerable<ResidenceViewModel>> GetResidencesByServiceContractIdAsync(Guid serviceContractId)
    {
        return await context.Residence
            .AsNoTracking()
            .Where(r => r.ServiceContractId == serviceContractId)
            .Include(r => r.Cohabitants)
            .Include(r => r.Keys)
                .ThenInclude(k => k.CurrentStatus)
            .ProjectTo<ResidenceViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ResidenceViewModel> GetResidenceByIdAsync(Guid residenceId)
    {
        return await context.Residence
            .AsNoTracking()
            .Where(r => r.Id == residenceId)
            .Include(r => r.Cohabitants)
            .ProjectTo<ResidenceViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException();
    }

    public async Task<IEnumerable<CohabitantViewModel>> GetCohabitantsByResidenceIdAsync(Guid residenceId)
    {
        return await context.Cohabitant
            .AsNoTracking()
            .Where(c => c.ResidenceId == residenceId)
            .Include(c => c.CohabitantType)
            .ProjectTo<CohabitantViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<KeyViewModel>> GetKeysByResidenceIdAsync(Guid residenceId)
    {
        return await context.Key
            .AsNoTracking()
            .Where(k => k.ResidenceId == residenceId)
            .Include(k => k.CurrentStatus)
            .ProjectTo<KeyViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<CohabitantViewModel> GetCohabitantByIdAsync(Guid cohabitantId)
    {
        return await context.Cohabitant
            .AsNoTracking()
            .Where(c => c.Id == cohabitantId)
            .Include(c => c.CohabitantType)
            .ProjectTo<CohabitantViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException();
    }
    public async Task<FullUserWithContractsViewModel> GetUserWithContractsByUserIdAsync(Guid userId)
    {
        // 1) Obtener el usuario en un solo query (si prefieres, 
        var userEntity = await userQueries.GetUserAsync(userId);

        // 2) Obtener los contratos de ese usuario
        var contracts = await context.ServiceContract
            .AsNoTracking()
            .Include(sc => sc.CurrentStatus)
            .Include(sc => sc.ServiceType)
            .Where(sc => sc.UserId == userId)
            .ProjectTo<BasicServiceContractViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        var userVm = mapper.Map<FullUserWithContractsViewModel>(userEntity);
        userVm.ServiceContracts = contracts;

        return userVm;
    }

    public async Task<IEnumerable<ServiceContractStatusReasonViewModel>> GetStatusReasonByStatusIdAsync(Guid statusId)
    {
        return await context.ServiceContractStatusReason
            .AsNoTracking()
            .Where(r => r.ServiceContractStatusId == statusId)
            .ProjectTo<ServiceContractStatusReasonViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceTypeViewModel>> GetAvailableServiceTypesAsync(Guid userId)
    {
        // Obtener los IDs de los tipos de servicio que ya están asociados a los contratos del usuario
        var userServiceTypeIds = await context.ServiceContract
            .AsNoTracking()
            .Where(sc => sc.UserId == userId)
            .Select(sc => sc.ServiceTypeId)
            .ToListAsync();

        // Obtener los tipos de servicio que no están en la lista de IDs obtenida anteriormente
        var availableServiceTypes = await context.ServiceType
            .AsNoTracking()
            .Where(st => !userServiceTypeIds.Contains(st.Id))
            .ProjectTo<ServiceTypeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return availableServiceTypes;
    }

    public async Task<IEnumerable<ServiceContractCentralUnitViewModel>> GetCentralUnitByServiceContractIdAsync(Guid serviceContractId)
    {
        return await context.ServiceContractCentralUnits
            .AsNoTracking()
            .Where(r => r.ServiceContractId == serviceContractId)
            .Include(r => r.CentralUnit)
            .Include(r => r.Peripherals)
            .ProjectTo<ServiceContractCentralUnitViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene una unidad central por su número de serie.
    /// </summary>
    /// <param name="serialNumber">Número de serie de la unidad central.</param>
    /// <returns>La unidad central correspondiente al número de serie proporcionado.</returns>
    public async Task<CentralUnitViewModel> GetCentralUnitBySerialNumberAsync(string serialNumber)
    {
        return await context.CentralUnits
            .AsNoTracking()
            .Where(cu => cu.SerialNumber == serialNumber)
            .ProjectTo<CentralUnitViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Central unit with serial number {serialNumber} not found.");
    }

    /// <summary>
    /// Obtiene un periférico por su número de serie.
    /// </summary>
    /// <param name="serialNumber">Número de serie del periférico.</param>
    /// <returns>El periférico correspondiente al número de serie proporcionado.</returns>
    public async Task<PeripheralViewModel> GetPeripheralBySerialNumberAsync(string serialNumber)
    {
        return await context.Peripheral
            .AsNoTracking()
            .Where(p => p.SerialNumber == serialNumber)
            .ProjectTo<PeripheralViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Peripheral with serial number {serialNumber} not found.");
    }

    public async Task<IEnumerable<PeripheralViewModel>> GetPeripheralsByCentralUnitIdAsync(Guid centralUnitId)
    {
        return await context.Peripheral
            .AsNoTracking()
            .Where(c => c.ServiceContractCentralUnitId == centralUnitId)
            .ProjectTo<PeripheralViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<KeyHistoryViewModel>> GetKeyHistoryByKeyIdAsync(Guid keyId)
    {
        return await context.KeyHistory
            .AsNoTracking()
            .Where(r => r.KeyId == keyId)
            .Include(r => r.KeyStatus)
            .ProjectTo<KeyHistoryViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene los usuarios beneficiarios de un contrato de servicio específico.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato de servicio.</param>
    /// <returns>Una lista de usuarios beneficiarios correspondientes al contrato de servicio proporcionado.</returns>
    public async Task<IEnumerable<BasicUserViewModel>> GetBeneficiariesByServiceContractAsync(Guid serviceContractId)
    {
        // Consulta para obtener los beneficiarios del contrato de servicio especificado
        return await context.ServiceContractBeneficiary
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(b => b.ServiceContractId == serviceContractId) // Filtra por el ID del contrato de servicio
            .Include(b => b.User) // Incluye la información del usuario
            .Select(b => b.User) // Selecciona la entidad User
            .ProjectTo<BasicUserViewModel>(mapper.ConfigurationProvider) // Proyecta los resultados al ViewModel correspondiente
            .ToListAsync(); // Convierte los resultados a una lista
    }


    /// <summary>
    /// Obtiene todos los registros de una tabla de maestros de forma genérica.
    /// </summary>
    public async Task<IEnumerable<TViewModel>> GetAllMastersAsync<T, TViewModel>()
    where T : class
    where TViewModel : class
    {
        return await context.Set<T>()
            .AsNoTracking()
            .ProjectTo<TViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<FullServiceContractViewModel> GetServiceContractBeneficiaryAsync(Guid serviceContractId, Guid beneficiaryId)
    {
        var serviceContractBeneficiary = await context.ServiceContractBeneficiary
            .AsNoTracking() // Mejora rendimiento en consultas de solo lectura
            .Where(c => c.ServiceContractId == serviceContractId && c.UserId == beneficiaryId)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException();

        var serviceContractViewModel = await GetServiceContractByIdAsync(serviceContractBeneficiary.ServiceContractId);
        var userViewModel = await userQueries.GetUserAsync(serviceContractBeneficiary.UserId);

        serviceContractViewModel.User = mapper.Map<FullUserViewModel>(userViewModel);

        return serviceContractViewModel;
    }

    public async Task<UsersByContractViewModel> GetUsersByContractAsync(Guid serviceContractId)
    {
        var sc = await context.ServiceContract
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Beneficiaries)
                .ThenInclude(b => b.User)
                    .ThenInclude(u => u.Identification)
            .FirstOrDefaultAsync(x => x.Id == serviceContractId)
            ?? throw new KeyNotFoundException($"ServiceContract {serviceContractId} not found.");

        // Mapeo del titular (user principal)
        var titularVm = await GetUserWithContractsByUserIdAsync(sc.UserId);

        // Mapeo de beneficiarios (solo datos básicos)
        var beneficiariesVm = sc.Beneficiaries
            .Select(b => mapper.Map<FullUserViewModel>(b.User));

        return new UsersByContractViewModel
        {
            Holder = titularVm,
            Beneficiaries = beneficiariesVm
        };
    }

}