using UserManagement.API.Application.Commands.AreaLevelCommands.CreateAreaLevel;
using UserManagement.API.Application.Commands.AreaLevelCommands.DeleteAreaLevel;
using UserManagement.API.Application.Commands.AreaLevelCommands.UpdateAreaLevel;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Common.Mapping.AreaLevelMappings;

public class ArealevelMappingProfile : Profile
{
    public ArealevelMappingProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<AreaLevel, AreaLevelViewModel>();
        #endregion

        #region Create Mapping (Creación)
        CreateMap<AreaLevelCreatedIntegrationEvent, CreateAreaLevelCommand>();
        CreateMap<CreateAreaLevelCommand, AreaLevel>();
        #endregion

        #region Update Mapping (Actualización)
        CreateMap<UpdateAreaLevelCommand, AreaLevel>();
        CreateMap<AreaLevelUpdatedIntegrationEvent, UpdateAreaLevelCommand>();
        #endregion

        #region Delete Mapping (Borrado)
        CreateMap<AreaLevelDeletedIntegrationEvent, DeleteAreaLevelCommand>();
        #endregion

    }
}