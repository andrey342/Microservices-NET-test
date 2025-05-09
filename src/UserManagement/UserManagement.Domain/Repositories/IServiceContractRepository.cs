using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Repositories;

public interface IServiceContractRepository : IRepository<ServiceContract>
{
    Task<ServiceContractStatus?> GetDefaultServiceContractStatusAsync();
    Task<ServiceContractStatus?> GetServiceContractStatusByIdAsync(Guid serviceContractStatusId);
    Task<ServiceContractStatusReason?> GetDefaultServiceContractStatusReasonAsync();
    Task<ServiceContractStatusReason?> GetServiceContractStatusReasonByIdAsync(Guid serviceContractStatusReasonId);
    Task<ServiceContractCentralUnit?> GetServiceContractCentralUnitByIdAsync(Guid id);
    Task<ServiceContractBeneficiary?> GetServiceContractBeneficiaryByIdAsync(Guid id);

    Task<Key?> GetKeyByIdAsync(Guid id);
    Task<KeyStatus?> GetKeyStatusByIdAsync(Guid id);
    Task<KeyStatus?> GetDefaultKeyStatusAsync();

    /// <summary>
    /// Obtiene una residencia por su ID.
    /// </summary>
    /// <param name="residenceId">ID de la residencia.</param>
    /// <returns>La residencia si existe, de lo contrario null.</returns>
    Task<Residence?> GetResidenceByIdAsync(Guid residenceId);
    Task<Cohabitant?> GetCohabitantByIdAsync(Guid cohabitantId);
    Task<ServiceContract?> GetByIdAsync(Guid id);
    void Add(ServiceContract serviceContract);
    void Update(ServiceContract serviceContract);
    void DeleteCohabitant(Cohabitant cohabitant);
    void DeleteKey(Key key);
    void UpdateCohabitant(Cohabitant cohabitant);
    void UpdateServiceContractCentralUnit(ServiceContractCentralUnit serviceContractCentral);
    void UpdateKey(Key key);
}
