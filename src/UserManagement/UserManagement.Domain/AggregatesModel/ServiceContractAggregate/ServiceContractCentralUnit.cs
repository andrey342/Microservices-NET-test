using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Events;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class ServiceContractCentralUnit : Entity
{
    public Guid CentralUnitId { get; private set; }
    public CentralUnit CentralUnit { get; private set; }
    public Guid ServiceContractId { get; private set; }
    public ServiceContract ServiceContract { get; private set; }

    //Lista de perifericos en esta entidad ya que un mismo unidad central puede tener varios contratos y tener un periferico por cada contrato.
    //Ej. casa compartida por 3 personas con 3 contratos y misma unidad central:
    //Contrato Jose: CentralUnit A con Periferico 01
    //Contrato Jesus: CentralUnitA con Periferico 02
    //Contrato Juan: CentralUnitA con Periferico 03
    private List<Peripheral> _peripherals = new List<Peripheral>();
    public IReadOnlyCollection<Peripheral> Peripherals => _peripherals;

    public ServiceContractCentralUnit() { }

    public ServiceContractCentralUnit(ServiceContractCentralUnit serviceContractCentralUnit)
    {
        this.CopyPropertiesTo(serviceContractCentralUnit);
    }

    public void AddPeripheral(Peripheral peripheral)
    {
        _peripherals.Add(peripheral);
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Device,
            UserHistoryConstants.Actions.New,
            $"Asignado periferico {peripheral.SerialNumber}",
            ServiceContract.UserId,
            ServiceContractId
        ));
    }

    public void RemovePeripheral(Peripheral peripheral)
    {
        _peripherals.Remove(peripheral);
        AddDomainEvent(new UserHistoryDomainEvent(
            UserHistoryConstants.Types.Device,
            UserHistoryConstants.Actions.Delete,
            $"Retirado periferico {peripheral.SerialNumber}",
            ServiceContract.UserId,
            ServiceContractId
        ));
    }

}
