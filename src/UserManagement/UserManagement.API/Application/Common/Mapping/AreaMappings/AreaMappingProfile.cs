using UserManagement.API.Application.Commands.AreaCommands.CreateArea;
using UserManagement.API.Application.Commands.AreaCommands.DeleteArea;
using UserManagement.API.Application.Commands.AreaCommands.UpdateArea;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Common.Mapping.AreaMappings;

public class AreaMappingProfile : Profile
{
    public AreaMappingProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<Area, AreaViewModel>();
        #endregion

        #region Create Mapping (Creación)
        CreateMap<AreaCreatedIntegrationEvent, CreateAreaCommand>();
        CreateMap<CreateAreaCommand, Area>();
        #endregion

        #region Update Mapping (Actualización)
        CreateMap<UpdateAreaCommand, Area>();
        CreateMap<AreaUpdatedIntegrationEvent, UpdateAreaCommand>();
        #endregion

        #region Delete Mapping (Borrado)
        CreateMap<AreaDeletedIntegrationEvent, DeleteAreaCommand>();
        #endregion

    }
}