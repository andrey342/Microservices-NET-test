namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

public record RoadType
{
    public string Name { get; private set; }

    private static readonly HashSet<string> ValidRoadTypes = new HashSet<string>
    {
        "Avenida",
        "Calle",
        "Carretera",
        "Paseo",
        "Plaza",
        "Camino",
        "Ronda",
        "Vía",
        "Boulevard",
        "Callejón",
        "Travesía",
        "Sendero",
        "Autopista",
        "Autovía",
        "Carrer",
        "Calleja",
        "Pasaje",
        "Puente",
        "Rotonda",
        "Urbanización"
    };

    public RoadType(string name)
    {
        if (!ValidRoadTypes.Contains(name))
        {
            throw new ArgumentException($"Invalid road type: {name}. Valid road types are: {string.Join(", ", ValidRoadTypes)}", nameof(name));
        }

        Name = name;
    }
    public static IEnumerable<string> GetRoadTypes() => ValidRoadTypes;

}
