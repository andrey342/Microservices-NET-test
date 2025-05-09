using AutoMapper;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Common.Mapping.Masters;

#region Animal Mapping
public class AnimalMappingProfile : Profile
{
    public AnimalMappingProfile()
    {
        CreateMap<Animal, AnimalViewModel>();
    }
}
#endregion

#region CivilStatus Mapping
public class CivilStatusMappingProfile : Profile
{
    public CivilStatusMappingProfile()
    {
        CreateMap<CivilStatus, CivilStatusViewModel>();
    }
}
#endregion

#region DependencyDegree Mapping
public class DependencyDegreeMappingProfile : Profile
{
    public DependencyDegreeMappingProfile()
    {
        CreateMap<DependencyDegree, DependencyDegreeViewModel>();
    }
}
#endregion

#region Education Mapping
public class EducationMappingProfile : Profile
{
    public EducationMappingProfile()
    {
        CreateMap<Education, EducationViewModel>();
    }
}
#endregion

#region IdentificationType Mapping
public class IdentificationTypeMappingProfile : Profile
{
    public IdentificationTypeMappingProfile()
    {
        CreateMap<IdentificationType, IdentificationTypeViewModel>().ReverseMap();
    }
}
#endregion

#region Language Mapping
public class LanguageMappingProfile : Profile
{
    public LanguageMappingProfile()
    {
        CreateMap<Language, LanguageViewModel>();
    }
}
#endregion

#region Sex Mapping
public class SexMappingProfile : Profile
{
    public SexMappingProfile()
    {
        CreateMap<Sex, SexViewModel>();
    }
}
#endregion

#region CohabitantType Mapping
public class CohabitantTypeProfile : Profile
{
    public CohabitantTypeProfile()
    {
        CreateMap<CohabitantType, CohabitantTypeViewModel>();
    }
}
#endregion

#region ServiceType Mapping
public class ServiceTypeProfile : Profile
{
    public ServiceTypeProfile()
    {
        CreateMap<ServiceType, ServiceTypeViewModel>();
    }
}
#endregion

#region ServiceContractStatus Mapping
public class ServiceContractStatusProfile : Profile
{
    public ServiceContractStatusProfile()
    {
        CreateMap<ServiceContractStatus, ServiceContractStatusViewModel>();
    }
}
#endregion

#region ServiceContractStatusReason Mapping
public class ServiceContractStatusReasonProfile : Profile
{
    public ServiceContractStatusReasonProfile()
    {
        CreateMap<ServiceContractStatusReason, ServiceContractStatusReasonViewModel>();
    }
}
#endregion

#region KeyStatus Mapping
public class KeyStatusProfile : Profile
{
    public KeyStatusProfile()
    {
        CreateMap<KeyStatus, KeyStatusViewModel>();
    }
}
#endregion