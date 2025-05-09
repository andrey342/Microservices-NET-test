using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class AllergyImpact : Entity
{
    public Guid MedicalInformationId { get; private set; }
    public MedicalInformation MedicalInformation { get; private set; }
    public Guid AllergyId { get; private set; }
    public Allergy Allergy { get; private set; }
    public Guid SeverityId { get; private set; }
    public AllergySeverity Severity { get; private set; }
    public string Reaction { get; private set; }

    private AllergyImpact() { }

    public AllergyImpact(AllergyImpact allergyImpact)
    {
        this.CopyPropertiesTo(allergyImpact);
    }

    public void Update(AllergyImpact allergyImpact)
    {
        this.CopyPropertiesTo(allergyImpact);
    }
}
