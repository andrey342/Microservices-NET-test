using UserManagement.API.Application.Queries.CentralUnitQueries;
using UserManagement.API.Application.Queries.PeripheralQueries;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Infrastructure.Migrations;

namespace UserManagement.API.Application.Queries.ServiceContractQueries;

public interface IServiceContractQueries
{
    /// <summary>
    /// Obtiene un contrato por su ID.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato.</param>
    /// <returns>El contrato correspondiente al ID proporcionado.</returns>
    Task<FullServiceContractViewModel> GetServiceContractByIdAsync(Guid serviceContractId);

    /// <summary>
    /// Obtiene todos los contratos de un centro de trabajo específico.
    /// </summary>
    /// <param name="workCenterId">ID del centro de trabajo.</param>
    /// <returns>Una lista de contratos correspondientes al centro de trabajo proporcionado.</returns>
    Task<IEnumerable<FullServiceContractViewModel>> GetServiceContractsByWorkCenterAsync(Guid workCenterId);

    /// <summary>
    /// Obtiene todos los contratos de usuarios en un centro de trabajo específico.
    /// </summary>
    /// <param name="workCenterId">ID del centro de trabajo.</param>
    /// <returns>Una lista de contratos de usuarios correspondientes al centro de trabajo proporcionado.</returns>
    Task<IEnumerable<BasicUserWithContractsViewModel>> GetAllUsersContractByWorkcenterIdAsync(Guid workCenterId);

    /// <summary>
    /// Obtiene todas las residencias asociadas a un contrato específico.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato.</param>
    /// <returns>Una lista de residencias correspondientes al contrato proporcionado.</returns>
    Task<IEnumerable<ResidenceViewModel>> GetResidencesByServiceContractIdAsync(Guid serviceContractId);

    /// <summary>
    /// Obtiene una residencia por su ID.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <returns>La residencia correspondiente al ID proporcionado.</returns>
    Task<ResidenceViewModel> GetResidenceByIdAsync(Guid residenceId);

    /// <summary>
    /// Obtiene todos los cohabitantes de una residencia específica.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <returns>Una lista de cohabitantes correspondientes a la residencia proporcionada.</returns>
    Task<IEnumerable<CohabitantViewModel>> GetCohabitantsByResidenceIdAsync(Guid residenceId);

    /// <summary>
    /// Obtiene todas las llaves asociadas a una residencia específica.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <returns>Una lista de llaves correspondientes a la residencia proporcionada.</returns>
    Task<IEnumerable<KeyViewModel>> GetKeysByResidenceIdAsync(Guid residenceId);

    /// <summary>
    /// Obtiene un cohabitante por su ID.
    /// </summary>
    /// <param name="cohabitantId">ID del cohabitante.</param>
    /// <returns>El cohabitante correspondiente al ID proporcionado.</returns>
    Task<CohabitantViewModel> GetCohabitantByIdAsync(Guid cohabitantId);

    /// <summary>
    /// Obtiene un usuario con contratos por su ID.
    /// </summary>
    /// <param name="userId">ID del usuario.</param>
    /// <returns>El usuario con contratos correspondiente al ID proporcionado.</returns>
    Task<FullUserWithContractsViewModel> GetUserWithContractsByUserIdAsync(Guid userId);

    /// <summary>
    /// Obtiene las razones de estado por el ID del estado.
    /// </summary>
    /// <param name="statusId">ID del estado.</param>
    /// <returns>Una lista de razones de estado correspondientes al ID del estado proporcionado.</returns>
    Task<IEnumerable<ServiceContractStatusReasonViewModel>> GetStatusReasonByStatusIdAsync(Guid statusId);

    /// <summary>
    /// Obtiene los tipos de servicio disponibles para un usuario específico.
    /// </summary>
    /// <param name="userId">ID del usuario.</param>
    /// <returns>Una lista de tipos de servicio disponibles para el usuario proporcionado.</returns>
    Task<IEnumerable<ServiceTypeViewModel>> GetAvailableServiceTypesAsync(Guid userId);

    /// <summary>
    /// Obtiene las unidades centrales de un contrato de servicio específico.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato de servicio.</param>
    /// <returns>Una lista de unidades centrales correspondientes al contrato de servicio proporcionado.</returns>
    Task<IEnumerable<ServiceContractCentralUnitViewModel>> GetCentralUnitByServiceContractIdAsync(Guid serviceContractId);

    /// <summary>
    /// Obtiene los periféricos de una unidad central específica.
    /// </summary>
    /// <param name="centralUnitId">ID de la unidad central.</param>
    /// <returns>Una lista de periféricos correspondientes a la unidad central proporcionada.</returns>
    Task<IEnumerable<PeripheralViewModel>> GetPeripheralsByCentralUnitIdAsync(Guid centralUnitId);

    /// <summary>
    /// Obtiene una unidad central por su número de serie.
    /// </summary>
    /// <param name="serialNumber">Número de serie de la unidad central.</param>
    /// <returns>La unidad central correspondiente al número de serie proporcionado.</returns>
    Task<CentralUnitViewModel> GetCentralUnitBySerialNumberAsync(string serialNumber);

    /// <summary>
    /// Obtiene un periférico por su número de serie.
    /// </summary>
    /// <param name="serialNumber">Número de serie del periférico.</param>
    /// <returns>El periférico correspondiente al número de serie proporcionado.</returns>
    Task<PeripheralViewModel> GetPeripheralBySerialNumberAsync(string serialNumber);

    /// <summary>
    /// Obtiene el historial de una llave por su ID.
    /// </summary>
    /// <param name="keyId">ID de la llave.</param>
    /// <returns>Una lista de historial de la llave correspondiente al ID proporcionado.</returns>
    Task<IEnumerable<KeyHistoryViewModel>> GetKeyHistoryByKeyIdAsync(Guid keyId);

    /// <summary>
    /// Obtiene los usuarios beneficiarios de un contrato de servicio específico.
    /// </summary>
    /// <param name="serviceContractId">ID del contrato de servicio.</param>
    /// <returns>Una lista de usuarios beneficiarios correspondientes al contrato de servicio proporcionado.</returns>
    Task<IEnumerable<BasicUserViewModel>> GetBeneficiariesByServiceContractAsync(Guid serviceContractId);

    /// <summary>
    /// Obtiene de forma genérica todos los registros de una tabla maestra y los proyecta a su ViewModel.
    /// </summary>
    Task<IEnumerable<TViewModel>> GetAllMastersAsync<T, TViewModel>()
        where T : class
        where TViewModel : class;

    Task<FullServiceContractViewModel> GetServiceContractBeneficiaryAsync(Guid serviceContractId, Guid beneficiaryId);
    Task<UsersByContractViewModel> GetUsersByContractAsync(Guid serviceContractId);
}

