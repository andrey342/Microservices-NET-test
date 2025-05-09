using AutoMapper;
using UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;
using UserManagement.API.Application.Commands.CentralUnitCommands.DeleteCentralUnit;
using UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;
using UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;
using UserManagement.API.Application.Commands.PeripheralCommands.DeletePeripheral;
using UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;
using UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;
using UserManagement.API.Application.Commands.ResidenceCommands.UpdateResidence;
using UserManagement.API.Application.Commands.ServiceContractCommands.AddBeneficiary;
using UserManagement.API.Application.Commands.ServiceContractCommands.AddCentralUnit;
using UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract;
using UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContract;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Queries.CentralUnitQueries;
using UserManagement.API.Application.Queries.PeripheralQueries;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.Common.Mapping.ServiceContractMappings;

public class ServiceContractMappingProfile : Profile
{
    public ServiceContractMappingProfile()
    {
        #region View Models Mapping (Lectura)
        CreateMap<ServiceContract, FullServiceContractViewModel>();
        CreateMap<ServiceContract, BasicServiceContractViewModel>();

        CreateMap<ServiceContract, BasicUserContractObjectsViewModel>()
            .ForMember(dest => dest.User,opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ContractStatusName, opt => opt.MapFrom(src => src.CurrentStatus.Name))
            .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.ServiceType.Name));

        CreateMap<Residence, ResidenceViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        CreateMap<Address, AddressViewModel>()
            .ForMember(dest => dest.RoadType, opt => opt.MapFrom(src => src.RoadType.Name))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.Name));
        CreateMap<ServiceContractCentralUnit, ServiceContractCentralUnitViewModel>();
        CreateMap<CentralUnit, CentralUnitViewModel>();
        CreateMap<Peripheral, PeripheralViewModel>();
        CreateMap<ServiceContractBeneficiary, BasicUserViewModel>();

        CreateMap<ServiceContractBeneficiary, ServiceContractBeneficiaryViewModel>();
        #endregion

        #region Create Mapping (Creación)
        CreateMap<CreateServiceContractRequest, ServiceContract>();
        CreateMap<CreateResidenceRequest, Residence>()
            .ForPath(dest => dest.Address.RoadType, opt => opt.MapFrom(src => new RoadType(src.Address.RoadType)))
            .ForPath(dest => dest.Address.StreetName, opt => opt.MapFrom(src => src.Address.StreetName))
            .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
            .ForPath(dest => dest.Address.Town, opt => opt.MapFrom(src => src.Address.Town))
            .ForPath(dest => dest.Address.Province, opt => opt.MapFrom(src => new Province(src.Address.Province)))
            .ForPath(dest => dest.Address.Door, opt => opt.MapFrom(src => src.Address.Door))
            .ForPath(dest => dest.Address.Floor, opt => opt.MapFrom(src => src.Address.Floor))
            .ForPath(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForPath(dest => dest.Address.Stair, opt => opt.MapFrom(src => src.Address.Stair))
            .ForPath(dest => dest.Address.Latitude, opt => opt.MapFrom(src => src.Address.Latitude))
            .ForPath(dest => dest.Address.Longitude, opt => opt.MapFrom(src => src.Address.Longitude));
        CreateMap<CreateCentralUnitRequest, CentralUnit>();
        CreateMap<CreatePeripheralRequest, Peripheral>();
        CreateMap<AddCentralUnitRequest, ServiceContractCentralUnit>();
        CreateMap<AddBeneficiaryRequest, ServiceContractBeneficiary>();
        CreateMap<AddressRequest, Address>()
            .ForMember(dest => dest.RoadType, opt => opt.MapFrom(src => new RoadType(src.RoadType)))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => new Province(src.Province)));

        CreateMap<CentralUnitCreatedIntegrationEvent, CreateCentralUnitRequest>();
        CreateMap<PeripheralCreatedIntegrationEvent, CreatePeripheralRequest>();

        #endregion

        #region Update Mapping (Actualizar)
        CreateMap<UpdateResidenceRequest, Residence>();
        CreateMap<UpdateResidenceCommand, Residence>();
        CreateMap<UpdateCentralUnitRequest, CentralUnit>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdatePeripheralRequest, Peripheral>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CentralUnitUpdatedIntegrationEvent, UpdateCentralUnitRequest>();
        CreateMap<PeripheralUpdatedIntegrationEvent, UpdatePeripheralRequest>();
        #endregion

        #region Update Mappin (Actualización)
        CreateMap<UpdateServiceContractRequest, ServiceContract>();
        CreateMap<UpdateResidenceRequest, Residence>();
        CreateMap<UpdateResidenceCommand, Residence>();

        #endregion

        #region Delete Mappin (Borrado)
        CreateMap<CentralUnitDeletedIntegrationEvent, DeleteCentralUnitCommand>();
        CreateMap<PeripheralDeletedIntegrationEvent, DeletePeripheralCommand>();
        #endregion
    }
}