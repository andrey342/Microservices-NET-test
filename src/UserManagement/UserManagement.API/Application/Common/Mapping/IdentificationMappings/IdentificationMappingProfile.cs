using AutoMapper;
using UserManagement.API.Application.Commands.IdentificationCommands.CreateIdentification;
using UserManagement.API.Application.Commands.IdentificationCommands.UpdateIdentification;
using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.API.Application.Commands.UserCommands.UpdateUser;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Common.Mapping.IdentificationMappings;

public class IdentificationMappingProfile : Profile
{
    public IdentificationMappingProfile()
    {
        #region View Models Mapping (Lectura)

        CreateMap<Identification, FullIdentificationViewModel>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.IdentificationType))
            .ReverseMap();

        #endregion

        #region Create Mapping (Creación)

        CreateMap<CreateIdentificationRequest, Identification>();

        #endregion

        #region Update Mapping (Actualización)

        CreateMap<UpdateIdentificationRequest, Identification>();

        #endregion
    }
}
