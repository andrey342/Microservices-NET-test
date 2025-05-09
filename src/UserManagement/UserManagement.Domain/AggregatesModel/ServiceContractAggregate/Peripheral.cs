using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class Peripheral : Entity
{
    public string Code { get; private set; }
    public string SerialNumber { get; private set; }
    public Guid? ServiceContractCentralUnitId { get; private set; }
    public ServiceContractCentralUnit? ServiceContractCentralUnit { get; private set; }

    public Peripheral() { }

    public Peripheral(Peripheral peripheral)
    {
        this.CopyPropertiesTo(peripheral);
    }

    public void Update(Peripheral peripheral)
    {
        this.CopyPropertiesTo(peripheral);
    }
}