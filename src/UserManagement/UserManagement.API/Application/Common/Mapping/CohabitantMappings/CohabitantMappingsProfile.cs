using AutoMapper;
using UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;
using UserManagement.API.Application.Commands.CohabitantCommands.UpdateCohabitant;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Common.Mapping.CohabitantMappings;

public class CohabitantMappingsProfile : Profile
{
    public CohabitantMappingsProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<Cohabitant, CohabitantViewModel>()
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.PhoneNumbers.MobilePhone));
        #endregion

        #region Create Mapping (Creación)
        CreateMap<CreateCohabitantRequest, Cohabitant>()
            .ForPath(dest => dest.PhoneNumbers.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        #endregion

        #region Update Mapping (Actualización)
        CreateMap<UpdateCohabitantRequest, Cohabitant>()
           .ForPath(dest => dest.PhoneNumbers.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        #endregion
    }
}