using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class Identification : Entity
{
    public string Number { get; private set; }
    public DateTime? ExpirationDate { get; private set; } = null;
    public DateTime? UpdateDate { get; private set; } = null;
    public Guid TypeId { get; private set; } // Clave foránea
    public IdentificationType IdentificationType { get; private set; } // Relación

    private Identification() { }

    public Identification(Identification identification)
    {
        this.CopyPropertiesTo(identification);
    }

    public void UpdateTypeAndDates(Guid typeId, DateTime? expiration, DateTime? update)
    {
        TypeId = typeId;
        ExpirationDate = expiration;
        UpdateDate = update;
    }

    public void UpdateType(Guid newTypeId)
    {
        TypeId = newTypeId;
    }
}
