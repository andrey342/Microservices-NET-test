using AutoMapper;
using UserManagement.API.Application.Commands.UserAnimalCommands.CreateUserAnimal;
using UserManagement.API.Application.Commands.UserAnimalCommands.UpdateUserAnimal;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Common.Mapping.UserAnimalMappings;

public class UserAnimalMappingProfile : Profile
{
    public UserAnimalMappingProfile()
    {
        #region View Models Mapping (Lectura)

        CreateMap<UserAnimal, FullUserAnimalViewModel>();

        #endregion

        #region Create Mapping (Creación)

        CreateMap<CreateUserAnimalRequest, UserAnimal>();

        #endregion

        #region Update Mapping (Actualización)

        CreateMap<UpdateUserAnimalRequest, UserAnimal>();

        #endregion
    }
}
