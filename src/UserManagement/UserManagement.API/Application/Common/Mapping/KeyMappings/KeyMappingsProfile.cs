using UserManagement.API.Application.Commands.KeyCommands.CreateKey;
using UserManagement.API.Application.Commands.KeyCommands.UpdateKey;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Common.Mapping.KeyMappings;

public class KeyMappingsProfile : Profile
{
    public KeyMappingsProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<Key, KeyViewModel>();

        CreateMap<KeyHistory, KeyHistoryViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.KeyStatus.Name));


        #endregion

        #region Create Mapping (Creación)
        CreateMap<CreateKeyRequest, Key>();
        #endregion

        #region Update Mapping (Actualización)
        CreateMap<UpdateKeyRequest, Key>();
        #endregion
    }
}