using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate;
public class PreferredProfessional : Entity
{
    public Guid ProfessionalId { get; private set; }
    public string Name { get; private set; }
    public string Surname1 { get; private set; }
    public string? Surname2 { get; private set; }

    private PreferredProfessional() { }

    public PreferredProfessional(PreferredProfessional preferredProfessional)
    {
        this.CopyPropertiesTo(preferredProfessional);
    }

    public void Update(string name, string surname1, string? surname2)
    {
        this.Name = name;
        this.Surname1 = surname1;
        this.Surname2 = surname2;
    }
}
