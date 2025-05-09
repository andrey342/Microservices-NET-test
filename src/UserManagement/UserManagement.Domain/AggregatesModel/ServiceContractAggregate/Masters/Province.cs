namespace UserManagement.Domain.AggregatesModel.ServiceContractAggregate.Masters;

public record Province
{
    public string Name { get; private set; }
    
    private static readonly HashSet<string> ValidProvinces = new HashSet<string>
    {
        "A Coruña",
        "Álava",
        "Albacete",
        "Alicante",
        "Almería",
        "Asturias",
        "Ávila",
        "Badajoz",
        "Balearic Islands",
        "Barcelona",
        "Burgos",
        "Cáceres",
        "Cádiz",
        "Cantabria",
        "Castellón",
        "Ciudad Real",
        "Córdoba",
        "Cuenca",
        "Gipuzkoa",
        "Girona",
        "Granada",
        "Guadalajara",
        "Huelva",
        "Huesca",
        "Jaén",
        "La Rioja",
        "Las Palmas",
        "León",
        "Lleida",
        "Lugo",
        "Madrid",
        "Málaga",
        "Murcia",
        "Navarre",
        "Ourense",
        "Palencia",
        "Pontevedra",
        "Salamanca",
        "Santa Cruz de Tenerife",
        "Segovia",
        "Seville",
        "Soria",
        "Tarragona",
        "Teruel",
        "Toledo",
        "Valencia",
        "Valladolid",
        "Zamora",
        "Zaragoza"
    };

    public Province(string name)
    {
        if (!ValidProvinces.Contains(name))
        {
            throw new ArgumentException($"Invalid province: {name}. Valid provinces are: {string.Join(", ", ValidProvinces)}", nameof(name));
        }

        Name = name;
    }

    public static IEnumerable<string> GetProvinces() => ValidProvinces;
}
