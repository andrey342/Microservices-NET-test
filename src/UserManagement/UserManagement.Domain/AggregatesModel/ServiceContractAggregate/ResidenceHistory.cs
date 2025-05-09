using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

public class ResidenceHistory : Entity
{
    public Residence Residence { get; private set; }
    public Guid ResidenceId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public ResidenceHistory() { }

    public ResidenceHistory(ResidenceHistory residenceHistory)
    {
        this.CopyPropertiesFast(residenceHistory);
        //Domain event change current residence to serviceContract
    }
}
