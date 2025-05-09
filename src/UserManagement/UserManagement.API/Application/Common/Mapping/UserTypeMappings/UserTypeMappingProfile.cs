using UserManagement.API.Application.Commands.UserTypeCommands.CreateUserType;
using UserManagement.API.Application.Commands.UserTypeCommands.DeleteUserType;
using UserManagement.API.Application.Commands.UserTypeCommands.UpdateUserType;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;

namespace UserManagement.API.Application.Common.Mapping.UserTypeMappings;

public class UserTypeMappingProfile : Profile
{
    public UserTypeMappingProfile()
    {
        CreateMap<UserType, UserTypeViewModel>();

        CreateMap<UserTypeCreatedIntegrationEvent, CreateUserTypeCommand>();
        CreateMap<CreateUserTypeCommand, UserType>();

        CreateMap<UserTypeUpdatedIntegrationEvent, UpdateUserTypeCommand>();
        CreateMap<UpdateUserTypeCommand, UserType>();

        CreateMap<UserTypeDeletedIntegrationEvent, DeleteUserTypeCommand>();
    }
}