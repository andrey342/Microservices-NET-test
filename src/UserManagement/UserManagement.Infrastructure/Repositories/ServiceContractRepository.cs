using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

namespace UserManagement.Infrastructure.Repositories;

public class ServiceContractRepository: IServiceContractRepository
{
    private readonly UserContext _context;

    public ServiceContractRepository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<ServiceContractStatus?> GetDefaultServiceContractStatusAsync()
    {
        return await _context.ServiceContractStatus
            .FirstOrDefaultAsync(cs => cs.Default == true);
    }

    public async Task<ServiceContractStatus?> GetServiceContractStatusByIdAsync(Guid serviceContractStatusId)
    {
        return await _context.ServiceContractStatus
            .FirstOrDefaultAsync(scs => scs.Id == serviceContractStatusId);
    }

    public async Task<ServiceContractStatusReason?> GetDefaultServiceContractStatusReasonAsync()
    {
        return await _context.ServiceContractStatusReason
            .Include(sr => sr.ServiceContractStatus)
            .FirstOrDefaultAsync(sr => sr.ServiceContractStatus.Default == true);
    }

    public async Task<ServiceContractStatusReason?> GetServiceContractStatusReasonByIdAsync(Guid serviceContractStatusReasonId)
    {
        return await _context.ServiceContractStatusReason
            .FirstOrDefaultAsync(scs => scs.Id == serviceContractStatusReasonId);
    }

    public async Task<Residence?> GetResidenceByIdAsync(Guid residenceId)
    {
        return await _context.Residence
            .Include(r => r.ServiceContract)
            .Include(r => r.Cohabitants)
            .FirstOrDefaultAsync(r => r.Id == residenceId);
    }

    public async Task<Cohabitant?> GetCohabitantByIdAsync(Guid cohabitantId)
    {
        return await _context.Cohabitant
            .FirstOrDefaultAsync(c => c.Id == cohabitantId);
    }

    public async Task<ServiceContract?> GetByIdAsync(Guid id)
    {
        return await _context.ServiceContract
            .Include(c => c.CurrentStatus)
            .Include(c => c.ServiceType)
            .Include(c => c.UserType)
            .Include(c => c.UserTypology)
            .Include(c => c.Residences)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ServiceContractCentralUnit?> GetServiceContractCentralUnitByIdAsync(Guid id)
    {
        return await _context.ServiceContractCentralUnits
            .Include(sccu => sccu.ServiceContract)
            .FirstOrDefaultAsync(cu => cu.Id == id);
    }

    public async Task<ServiceContractBeneficiary?> GetServiceContractBeneficiaryByIdAsync(Guid id)
    {
        return await _context.ServiceContractBeneficiary
            .Include(sccu => sccu.ServiceContract)
            .FirstOrDefaultAsync(cu => cu.Id == id);
    }


    public async Task<Key?> GetKeyByIdAsync(Guid id)
    {
        return await _context.Key
             .Include(k => k.Residence)
                .ThenInclude(r => r.ServiceContract)
            .FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task<KeyStatus?> GetKeyStatusByIdAsync(Guid id)
    {
        return await _context.KeyStatus
            .FirstOrDefaultAsync(ks => ks.Id == id);
    }

    public async Task<KeyStatus?> GetDefaultKeyStatusAsync()
    {
        return await _context.KeyStatus
            .FirstOrDefaultAsync(ks => ks.Default == true);
    }

    public void Add(ServiceContract serviceContract)
    {
        _context.ServiceContract.Add(serviceContract);
    }

    public void Update(ServiceContract serviceContract)
    {
        _context.Attach(serviceContract).State = EntityState.Modified;
    }

    public void DeleteCohabitant(Cohabitant cohabitant)
    {
        _context.Cohabitant.Remove(cohabitant);
    }
    public void DeleteKey(Key key)
    {
        _context.Key.Remove(key);
    }
    public void UpdateCohabitant(Cohabitant cohabitant)
    {
        _context.Cohabitant.Update(cohabitant);
    }
    public void UpdateServiceContractCentralUnit(ServiceContractCentralUnit serviceContractCentral)
    {
        _context.ServiceContractCentralUnits.Update(serviceContractCentral);
    }
    public void UpdateKey(Key key)
    {
        _context.Key.Update(key);
    }
}
