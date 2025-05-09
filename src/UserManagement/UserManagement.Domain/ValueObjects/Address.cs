using System.Text;
using UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.ValueObjects;

public class Address : ValueObject
{
    public RoadType RoadType { get; private set; }
    public string StreetName { get; private set; }
    public string PostalCode { get; private set; }
    public string Town { get; private set; }
    public Province Province { get; private set; }
    public string? Door { get; private set; }
    public string? Floor { get; private set; }
    public string? Number { get; private set; }
    public string? Stair { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }

    public Address(){}

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{RoadType.Name} {StreetName}, ");
        if (!string.IsNullOrEmpty(Number)) sb.Append($"No. {Number}, ");
        if (!string.IsNullOrEmpty(Floor)) sb.Append($"Planta {Floor}, ");
        if (!string.IsNullOrEmpty(Door)) sb.Append($"Puerta {Door}, ");
        if (!string.IsNullOrEmpty(Stair)) sb.Append($"Escalera {Stair}, ");
        sb.Append($"{PostalCode} {Town}, {Province.Name}");
        return sb.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RoadType;
        yield return StreetName;
        yield return PostalCode;
        yield return Town;
        yield return Province;
        yield return Door;
        yield return Floor;
        yield return Number;
        yield return Stair;
        yield return Latitude;
        yield return Longitude;
    }
}
