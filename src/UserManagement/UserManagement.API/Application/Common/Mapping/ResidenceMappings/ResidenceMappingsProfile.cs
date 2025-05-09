using AutoMapper;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.API.Application.Common.Mapping.ResidenceMappings;
public class ResidenceMappingsProfile : Profile
{
    public ResidenceMappingsProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<Residence, ResidenceViewModel>();
        #endregion

        #region Create Mapping (Creación)
        //CreateMap<CreateServiceContractCommand, ServiceContract>();
        #endregion

        #region Update Mapping (Actualización)
        #endregion
    }
}
