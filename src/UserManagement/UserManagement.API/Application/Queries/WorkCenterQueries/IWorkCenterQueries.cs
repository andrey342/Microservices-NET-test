namespace UserManagement.API.Application.Queries.WorkCenterQueries
{
    public interface IWorkCenterQueries
    {
        Task<IEnumerable<UserTypeViewModel>> GetUserTypesByWorkCenterAsync(Guid workCenterId);
        Task<IEnumerable<UserTypologyViewModel>> GetUserTypologiesByWorkCenterAsync(Guid workCenterId);
        Task<IEnumerable<AreaViewModel>> GetAreasByAreaLevelIdAsync(Guid id);
        Task<IEnumerable<AreaLevelViewModel>> GetAreaLevelsByWorkCenterIdAsync(Guid id);
        Task<IEnumerable<ResourceBasicViewModel>> GetResourcesByWorkCenterIdByUserIdAsync(Guid workcenterId);
    }
}
