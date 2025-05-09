using UserManagement.Domain.SeedWork;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class Residence : Entity
{
    public ServiceContract ServiceContract { get; private set; }
    public Guid ServiceContractId { get; private set; }
    public Address Address { get; private set; }
    public bool Elevator { get; private set; }
    public bool Concierge { get; private set; }
    public bool Doorman { get; private set; }
    public bool FireHydrant { get; private set; }
    public bool Wifi { get; private set; }
    public bool Gas { get; private set; }
    public bool Electricity { get; private set; }
    public bool Water { get; private set; }
    public bool Internet { get; private set; }
    public string? ArchitecturalBarrierEntrance { get; private set; }
    public string? ArchitecturalBarriereResidence { get; private set; }
    public string? Observation { get; private set; }
    public bool IsCurrentResidence { get; private set; }
    private List<Key> _keys = new List<Key>();
    public IReadOnlyCollection<Key> Keys => _keys.AsReadOnly();

    private List<Cohabitant> _cohabitants = new List<Cohabitant>();
    public IReadOnlyCollection<Cohabitant> Cohabitants => _cohabitants.AsReadOnly();

    public Residence() { }

    public Residence(Residence residence)
    {
        this.CopyPropertiesFast(residence);
    }

    public void Update(Residence residence)
    {
        this.CopyPropertiesFast(residence);
    }

    public void setCurrentResidence(bool isCurrentResidence)
    {
        IsCurrentResidence = isCurrentResidence;
    }

    public string GetFullAddress()
    {
        return Address.ToString();
    }

    #region Cohabitant
    public void AddCohabitant(Cohabitant cohabitant)
    {
        if (_cohabitants.Any(c => c.Id == cohabitant.Id))
        {
            throw new InvalidOperationException("Cohabitant already exists in this residence.");
        }

        _cohabitants.Add(cohabitant);
    }

    public void RemoveCohabitant(Guid cohabitantId)
    {
        var cohabitant = _cohabitants.FirstOrDefault(c => c.Id == cohabitantId);
        if (cohabitant == null)
            throw new InvalidOperationException("Cohabitant not found in this residence.");

        _cohabitants.Remove(cohabitant);
    }

    public void UpdateCohabitant(Cohabitant cohabitant)
    {
        var oldCohabitant = _cohabitants.FirstOrDefault(c => c.Id == cohabitant.Id);
        if (cohabitant == null)
            throw new InvalidOperationException("Cohabitant not found in this residence.");

        oldCohabitant.Update(cohabitant);
    }
    #endregion

    #region Key
    public void AddKey(Key key)
    {
        if (_keys.Any(c => c.Id == key.Id))
        {
            throw new InvalidOperationException("Key already exists in this residence.");
        }

        _keys.Add(key);
    }

    public void RemoveKey(Guid keyId)
    {
        var key = _keys.FirstOrDefault(c => c.Id == keyId);
        if (key == null)
            throw new InvalidOperationException("Key not found in this residence.");

        _keys.Remove(key);
    }

    public void UpdateKey(Key key)
    {
        var oldKey = _keys.FirstOrDefault(c => c.Id == key.Id);
        if (key == null)
            throw new InvalidOperationException("Key not found in this residence.");

        oldKey.Update(key);
    }
    #endregion
}
