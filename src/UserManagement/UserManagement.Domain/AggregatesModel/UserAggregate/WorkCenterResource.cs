using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.AggregatesModel.UserAggregate
{
    public class WorkCenterResource : Entity
    {
        public string Observations { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public Resource Resource { get; private set; }
        public Guid ResourceId { get; private set; }

        public WorkCenterResource() { }

        public WorkCenterResource(WorkCenterResource workCenterResource)
        {
            this.CopyPropertiesTo(workCenterResource);
        }

        public void Update(WorkCenterResource workCenterResource)
        {
            this.CopyPropertiesTo(workCenterResource);
        }
    }
}
