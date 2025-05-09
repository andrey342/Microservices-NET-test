using UserManagement.Domain.SeedWork;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.AggregatesModel.WorkCenterAggregate
{
    public class Resource : Entity
    {
        public string Name { get; private set; }
        public PhoneNumbers PhoneNumbers { get; private set; }
        public Guid WorkCenterId { get; private set; }

        public Resource() { }

        public Resource(Resource resource)
        {
            this.CopyPropertiesTo(resource);
        }
    }
}
