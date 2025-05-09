using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class Key : Entity
{
    public Residence Residence { get; private set; }
    public Guid ResidenceId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Keys { get; private set; }
    public KeyStatus CurrentStatus { get; private set; }
    public Guid CurrentStatusId { get; private set; }
    private List<KeyHistory> _keyHistories = new List<KeyHistory>();
    public IReadOnlyCollection<KeyHistory> KeyHistories => _keyHistories.AsReadOnly();
    public Key() { }

    public Key(Key key)
    {
        this.CopyPropertiesTo(key);
    }

    public void Update(Key key)
    {
        this.CopyPropertiesTo(key);
    }

    public void AddKeyHistory(KeyStatus keyStatus)
    {

        var historyEntry = new KeyHistory(this.Id, keyStatus.Id);

        //Añadir cambio de estado a historico de llave
        _keyHistories.Add(historyEntry);

        //Actualizar el estado actual
        CurrentStatusId = keyStatus.Id;
        CurrentStatus = keyStatus;
    }
}
