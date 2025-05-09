using UserManagement.API.Application.Commands.UserTypologyCommands.CreateUserTypology;
using UserManagement.API.Application.Commands.UserTypologyCommands.DeleteUserTypology;
using UserManagement.API.Application.Commands.UserTypologyCommands.UpdateUserTypology;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Common.Mapping.UserTypologyMappings;

public class UserTypologyMappingProfile : Profile
{
    public UserTypologyMappingProfile()
    {
        CreateMap<UserTypology, UserTypologyViewModel>();

        CreateMap<UserTypologyCreatedIntegrationEvent, CreateUserTypologyCommand>();
        CreateMap<CreateUserTypologyCommand, UserTypology>();

        CreateMap<UserTypologyUpdatedIntegrationEvent, UpdateUserTypologyCommand>();
        CreateMap<UpdateUserTypologyCommand, UserTypology>();

        CreateMap<UserTypologyDeletedIntegrationEvent, DeleteUserTypologyCommand>();
    }
}