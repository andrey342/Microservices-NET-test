using AutoMapper.QueryableExtensions;

namespace UserManagement.API.Application.Queries.WorkCenterQueries;

public class WorkCenterQueries(UserContext context, IMapper mapper)
: IWorkCenterQueries
{
    public async Task<IEnumerable<UserTypeViewModel>> GetUserTypesByWorkCenterAsync(Guid workCenterId)
    {
        return await context.UserType
            .AsNoTracking()
            .Where(r => r.WorkCenterId == workCenterId)
            .ProjectTo<UserTypeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserTypologyViewModel>> GetUserTypologiesByWorkCenterAsync(Guid workCenterId)
    {
        return await context.UserTypology
            .AsNoTracking()
            .Where(r => r.WorkCenterId == workCenterId)
            .ProjectTo<UserTypologyViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<AreaViewModel>> GetAreasByAreaLevelIdAsync(Guid id)
    {
        return await context.Area
            .AsNoTracking()
            .Where(a => a.AreaLevelId == id)
            .ProjectTo<AreaViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<AreaLevelViewModel>> GetAreaLevelsByWorkCenterIdAsync(Guid id)
    {
        return await context.AreaLevel
            .AsNoTracking()
            .Where(al => al.WorkCenterId == id)
            .ProjectTo<AreaLevelViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ResourceBasicViewModel>> GetResourcesByWorkCenterIdByUserIdAsync(Guid workcenterId)
    {
        return await context.Resource
            .AsNoTracking()
            .Where(al => al.WorkCenterId == workcenterId)
            .ProjectTo<ResourceBasicViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}