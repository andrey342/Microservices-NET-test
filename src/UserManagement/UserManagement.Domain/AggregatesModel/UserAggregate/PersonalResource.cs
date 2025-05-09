using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.SeedWork;
using UserManagement.Domain.ValueObjects;

public class PersonalResource : Entity
{
    public string Name { get; set; }
    public string Observations { get; set; }
    public PhoneNumbers PhoneNumbers { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public PersonalResource() { }
    public PersonalResource(PersonalResource personalResource) { 
        this.CopyPropertiesTo(personalResource);
    }
    public void Update(PersonalResource personalResource) {
        this.CopyPropertiesTo(personalResource);
    }
}
