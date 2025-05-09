using UserManagement.API.Application.Queries.CentralUnitQueries;
using UserManagement.API.Application.Queries.PeripheralQueries;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

namespace UserManagement.API.Application.Queries.ServiceContractQueries;

#region ServiceContract View Models
public record FullServiceContractViewModel
{
    public Guid Id { get; set; }
    public Guid WorkCenterId { get; set; }
    public Guid UserId { get; set; }
    public FullUserViewModel User { get; set; }
    public Guid ServiceTypeId { get; set; }
    public ServiceTypeViewModel ServiceType { get; private set; }
    public Guid CurrentStatusId { get; set; }
    public ServiceContractStatusViewModel CurrentStatus { get; private set; }
    public Guid UserTypeId { get; set; }
    public UserTypeViewModel UserType { get; private set; }
    public Guid UserTypologyId { get; set; }
    public UserTypologyViewModel UserTypology { get; private set; }
    public List<ResidenceViewModel>? Residences { get; set; } = null;
    public List<ServiceContractCentralUnitViewModel>? ServiceContractCentralUnits { get; set; } = null;
}

public record BasicServiceContractViewModel
{
    public Guid Id { get; set; }
    public ServiceTypeViewModel ServiceType { get; private set; }
    public ServiceContractStatusViewModel CurrentStatus { get; private set; }
    public UserTypeViewModel UserType { get; private set; }
    public UserTypologyViewModel UserTypology { get; private set; }
    public List<ServiceContractBeneficiaryViewModel> Beneficiaries { get; set; } = new();

}

public record FullUserWithContractsViewModel : FullUserViewModel
{
    public List<BasicServiceContractViewModel>? ServiceContracts { get; set; } = null;
}

public record BasicUserWithContractsViewModel : BasicUserViewModel
{
    // Campo 1: nombres de los estados de contrato (separados por coma)
    public string ContractStatuses { get; set; } = string.Empty;

    // Campo 2: serviceTypeName + "." + statusName (separados por coma)
    public string ServiceStatuses { get; set; } = string.Empty;
    public List<Guid> Contracts { get; set; } = new();
}

public record BasicUserContractObjectsViewModel
{
    public Guid Id { get; set; }
    public BasicUserViewModel User { get; set; } = null!;
    public string? ContractStatusName { get; set; }
    public string? ServiceTypeName { get; set; }
}

public record CreatedContractViewModel
{
    public Guid ContractId { get; set; }
    public Guid UserId { get; set; }
}

public record UsersByContractViewModel
{
    /// <summary>El usuario titular del contrato</summary>
    public FullUserWithContractsViewModel Holder { get; init; } = null!;

    /// <summary>Los usuarios beneficiarios asociados al contrato</summary>
    public IEnumerable<FullUserViewModel> Beneficiaries { get; init; } = Enumerable.Empty<FullUserViewModel>();
}

#endregion

#region Relaciones

#region Residence View Models

public record ResidenceViewModel
{
    public Guid Id { get; set; }
    public Guid ServiceContractId { get; set; }
    public AddressViewModel Address { get; set; }
    public bool Elevator { get; set; }
    public bool Concierge { get; set; }
    public bool Doorman { get; set; }
    public bool FireHydrant { get; set; }
    public bool Wifi { get; set; }
    public bool Gas { get; set; }
    public bool Electricity { get; set; }
    public bool Water { get; set; }
    public bool Internet { get; set; }
    public string? ArchitecturalBarrierEntrance { get; set; }
    public string? ArchitecturalBarriereResidence { get; set; }
    public string? Observation { get; set; }
    public bool IsCurrentResidence { get; set; }
    public List<CohabitantViewModel>? Cohabitants { get; set; } = null;
    public List<KeyViewModel>? Keys { get; set; } = null;
}

public record AddressViewModel
{
    public string RoadType { get; set; }
    public string StreetName { get; set; }
    public string PostalCode { get; set; }
    public string Town { get; set; }
    public string Province { get; set; }
    public string? Door { get; set; }
    public string? Floor { get; set; }
    public string? Number { get; set; }
    public string? Stair { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
#endregion

#region Cohabitant View Models
public record CohabitantViewModel
{
    public Guid Id { get; set; }
    public Guid CohabitantTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname1 { get; set; } = null!;
    public string? Surname2 { get; set; }
    public string? Mobile { get; set; }
    public string? Observation { get; set; }
    public bool Resource { get; set; }
    public CohabitantTypeViewModel CohabitantType { get; set; } = null!;
}
#endregion

#region Key View Models
public record KeyViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Keys { get; set; }
    public Guid CurrentStatusId { get; set; }
    public KeyStatusViewModel CurrentStatus { get; set; } = null!;
}

public record KeyHistoryViewModel
{
    public string Status { get; set; } = null!;
    public DateTime Date { get; set; }
    public string DateFormatted => Date.ToString("HH:mm dd/MM/yyyy");
}
#endregion

#region ServiceContractStatusReason View Models
public record ServiceContractStatusReasonViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
#endregion

#region ServiceContractCentralUnit View Models

public record ServiceContractCentralUnitViewModel
{
    public Guid Id { get; set; }
    public Guid CentralUnitId { get; set; }
    public CentralUnitViewModel CentralUnit { get; set; }
    public Guid ServiceContractId { get; set; }
    public List<PeripheralViewModel>? Peripherals { get; set; } = null;
    public string PeripheralSerialNumbers => Peripherals != null
        ? string.Join(", ", Peripherals.Select(p => p.SerialNumber))
        : string.Empty;
}
#endregion

#region ServiceContractBeneficiary View Models
public record ServiceContractBeneficiaryViewModel
{
    public Guid Id { get; set; }
    public Guid ServiceContractId { get; set; }
    public Guid UserId { get; set; }
    public FullUserViewModel User { get; set; } = null!;
}
#endregion

#endregion

#region Maestros
/// <summary>
/// Clases individuales para cada maestro, heredando de MasterViewModel.
/// </summary>
public record CohabitantTypeViewModel : MasterViewModel;
public record WorkCenterUmViewModel : MasterViewModel;
public record ServiceTypeViewModel : MasterViewModel;
public record ServiceContractStatusViewModel : MasterViewModel
{
    public bool Default { get; private set; }
}
public record KeyStatusViewModel : MasterViewModel
{
    public bool Default { get; private set; }
}
#endregion