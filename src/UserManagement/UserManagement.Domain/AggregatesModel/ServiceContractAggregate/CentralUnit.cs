using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class CentralUnit : Entity
{
    public string Code { get; private set; }
    public string SerialNumber { get; private set; }
    public string Phone { get; private set; }
    private List<ServiceContractCentralUnit> _serviceContractCentralUnits = new List<ServiceContractCentralUnit>();
    public IReadOnlyCollection<ServiceContractCentralUnit> ServiceContractCentralUnits => _serviceContractCentralUnits;

    public CentralUnit() { }

    public CentralUnit(CentralUnit centralUnit)
    {
        this.CopyPropertiesTo(centralUnit);
    }

    public void Update(CentralUnit centralUnit)
    {
        this.CopyPropertiesTo(centralUnit);
    }
}