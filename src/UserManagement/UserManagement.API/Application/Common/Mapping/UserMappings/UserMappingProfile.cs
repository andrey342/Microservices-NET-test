using AutoMapper;
using UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;
using UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;
using UserManagement.API.Application.Commands.PersonalResourceCommands.UpdatePersonalResource;
using UserManagement.API.Application.Commands.PreferredProfessionalCommands.CreatePreferredProfessional;
using UserManagement.API.Application.Commands.PreferredProfessionalCommands.UpdatePreferredProfessional;
using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.API.Application.Commands.UserCommands.UpdateUser;
using UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;
using UserManagement.API.Application.Commands.WorkCenterResourceCommands.UpdateWorkCenterResource;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.WorkCenterAggregate;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.Common.Mapping.UserMappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        #region View Models Mapping (Lectura)

        CreateMap<User, FullUserViewModel>()
            .ForMember(dest => dest.CallTime, opt => opt.MapFrom(src => src.CallTime.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumbers.HomePhone))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.PhoneNumbers.MobilePhone));

        CreateMap<User, BasicUserViewModel>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Identification.Number));
        CreateMap<BasicUserViewModel, BasicUserWithContractsViewModel>();
        CreateMap<User, BasicUserWithContractsViewModel>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Identification.Number));
        CreateMap<FullUserViewModel, FullUserWithContractsViewModel>();
        CreateMap<PreferredProfessional, PreferredProfessionalViewModel>();

        CreateMap<WorkCenterResource, WorkCenterResourceViewModel>();
        CreateMap<PersonalResource, PersonalResourceViewModel>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumbers.HomePhone))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.PhoneNumbers.MobilePhone));
        CreateMap<CreateWorkCenterResourceRequest, WorkCenterResource>();
        CreateMap<UpdateWorkCenterResourceRequest, WorkCenterResource>();
        #endregion

        #region Create Mapping (Creación)

        CreateMap<CreateUserRequest, User>()
            .ForPath(dest => dest.PhoneNumbers.HomePhone, opt => opt.MapFrom(src => src.Phone))
            .ForPath(dest => dest.PhoneNumbers.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        
        CreateMap<CreatePreferredProfessionalRequest, PreferredProfessional>();
        CreateMap<CreatePersonalResourceRequest, PersonalResource>()
            .ForPath(dest => dest.PhoneNumbers.HomePhone, opt => opt.MapFrom(src => src.Phone))
            .ForPath(dest => dest.PhoneNumbers.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        #endregion

        #region Update Mapping (Actualización)

        CreateMap<UpdateUserRequest, User>()
            .ForPath(dest => dest.PhoneNumbers.HomePhone, opt => opt.MapFrom(src => src.Phone))
            .ForPath(dest => dest.PhoneNumbers.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        CreateMap<UpdatePreferredProfessionalCommand, PreferredProfessional>();
        CreateMap<ProfessionalUpdatedIntegrationEvent, UpdatePreferredProfessionalCommand>();

        CreateMap<UpdatePersonalResourceRequest, PersonalResource>()
            .ForPath(dest => dest.PhoneNumbers.HomePhone, opt => opt.MapFrom(src => src.Phone))
            .ForPath(dest => dest.PhoneNumbers.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        #endregion
    }
}
