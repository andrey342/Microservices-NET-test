using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;

public class CreateResidenceRequest
{
    public Guid ServiceContractId { get; set; }
    public AddressRequest Address { get; set; }
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
}

public class AddressRequest
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