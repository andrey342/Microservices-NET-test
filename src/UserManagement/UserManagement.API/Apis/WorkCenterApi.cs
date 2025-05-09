using Microsoft.AspNetCore.Http.HttpResults;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.API.Application.Queries.WorkCenterQueries;

namespace UserManagement.API.Apis;

public static class WorkCenterApi
{
    public static RouteGroupBuilder MapWorkCentersApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("workcenter-um").HasApiVersion(1.0);

        #region Apis principales

        api.MapGet("/getUserTypesByWorkCenter/{workCenterId:Guid}", GetUserTypesByWorkCenterAsync);
        api.MapGet("/getUserTypologiesByWorkCenter/{workCenterId:Guid}", GetUserTypologiesByWorkCenterAsync);
        api.MapGet("/getAreasByAreaLevel/{areaLevelId:Guid}", GetAreasByAreaLevelAsync);
        api.MapGet("/getAreaLevelsByWorkCenter/{workCenterId:Guid}", GetAreaLevelsByWorkCenterAsync);
        // Resources
        api.MapGet("/getResourcesByWorkcenterId/{workcenterId:Guid}", GetResourcesByWorkCenterIdByUserIdAsync);

        #endregion

        return api;
    }

    #region Queries

    /// <summary>
    /// Retrieves user types by work center ID.
    /// </summary>
    /// <param name="workCenterId">The ID of the work center.</param>
    /// <param name="queries">The work center queries service.</param>
    /// <returns>A list of user types associated with the work center.</returns>
    public static async Task<Ok<IEnumerable<UserTypeViewModel>>> GetUserTypesByWorkCenterAsync(
        Guid workCenterId, [FromServices] IWorkCenterQueries queries)
    {
        var userTypes = await queries.GetUserTypesByWorkCenterAsync(workCenterId);
        return TypedResults.Ok(userTypes);
    }

    /// <summary>
    /// Retrieves user typologies by work center ID.
    /// </summary>
    /// <param name="workCenterId">The ID of the work center.</param>
    /// <param name="queries">The work center queries service.</param>
    /// <returns>A list of user typologies associated with the work center.</returns>
    public static async Task<Ok<IEnumerable<UserTypologyViewModel>>> GetUserTypologiesByWorkCenterAsync(
        Guid workCenterId, [FromServices] IWorkCenterQueries queries)
    {
        var userTypologies = await queries.GetUserTypologiesByWorkCenterAsync(workCenterId);
        return TypedResults.Ok(userTypologies);
    }

    /// <summary>
    /// Retrieves areas by work center ID.
    /// </summary>
    /// <param name="workCenterId">The ID of the work center.</param>
    /// <param name="queries">The work center queries service.</param>
    /// <returns>A list of areas associated with the work center.</returns>
    public static async Task<Ok<IEnumerable<AreaViewModel>>> GetAreasByAreaLevelAsync(
        Guid areaLevelId, [FromServices] IWorkCenterQueries queries)
    {
        var areas = await queries.GetAreasByAreaLevelIdAsync(areaLevelId);
        return TypedResults.Ok(areas);
    }

    /// <summary>
    /// Retrieves area levels by work center ID.
    /// </summary>
    /// <param name="workCenterId">The ID of the work center.</param>
    /// <param name="queries">The work center queries service.</param>
    /// <returns>A list of area levels associated with the work center.</returns>
    public static async Task<Ok<IEnumerable<AreaLevelViewModel>>> GetAreaLevelsByWorkCenterAsync(
        Guid workCenterId, [FromServices] IWorkCenterQueries queries)
    {
        var areaLevels = await queries.GetAreaLevelsByWorkCenterIdAsync(workCenterId);
        return TypedResults.Ok(areaLevels);
    }

    public static async Task<Results<Ok<IEnumerable<ResourceBasicViewModel>>, NotFound<string>>> GetResourcesByWorkCenterIdByUserIdAsync(
    Guid workcenterId,
    [FromServices] IWorkCenterQueries queries)
    {
        try
        {
            var result = await queries.GetResourcesByWorkCenterIdByUserIdAsync(workcenterId);
            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }


    #endregion
}