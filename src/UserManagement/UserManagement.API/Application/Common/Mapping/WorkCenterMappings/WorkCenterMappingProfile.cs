using UserManagement.API.Application.Commands.WorkCenterCommands.CreateWorkCenter;
using UserManagement.API.Application.Commands.WorkCenterCommands.UpdateWorkCenter;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.Common.Mapping.WorkCenterMappings;
public class WorkCenterMappingProfile : Profile
{
    public WorkCenterMappingProfile()
    {
        CreateMap<WorkCenter, WorkCenterUmViewModel>();
        CreateMap<CreateWorkCenterCommand, WorkCenter>();
        CreateMap<UpdateWorkCenterCommand, WorkCenter>();

        CreateMap<Resource, ResourceBasicViewModel>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumbers.HomePhone))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.PhoneNumbers.MobilePhone));
    }
}
