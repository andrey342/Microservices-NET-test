using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class KeyHistory : Entity
{
    public Guid KeyId { get; private set; }
    public Key Key { get; private set; }
    public Guid KeyStatusId { get; private set; }
    public KeyStatus KeyStatus { get; private set; }
    public DateTime Date { get; private set; }

    public KeyHistory() { }

    public KeyHistory(Guid keyId, Guid keyStatusId)
    {
        KeyId = keyId;
        KeyStatusId = keyStatusId;
        Date = DateTime.UtcNow;
    }
}
