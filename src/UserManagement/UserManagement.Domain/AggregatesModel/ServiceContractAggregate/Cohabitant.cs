using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.SeedWork;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class Cohabitant: Entity
{
    public Guid ResidenceId { get; private set; }
    public Residence Residence { get; private set; }
    public string Name { get; private set; }
    public string Surname1 { get; private set; }
    public string? Surname2 { get; private set; }
    public PhoneNumbers? PhoneNumbers { get; private set; }
    public string? Observation { get; private set; }
    public Guid CohabitantTypeId { get; private set; }
    public CohabitantType CohabitantType { get; private set; }
    public bool Resource { get; private set; }

    private Cohabitant() { }

    public Cohabitant(Cohabitant cohabitant)
    {
        this.CopyPropertiesTo(cohabitant);
    }

    public void Update(Cohabitant cohabitant)
    {
        this.CopyPropertiesTo(cohabitant);
    }
}