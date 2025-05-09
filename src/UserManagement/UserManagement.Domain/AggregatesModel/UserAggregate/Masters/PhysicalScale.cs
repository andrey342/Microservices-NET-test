namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

public record PhysicalScale
{
    public string Name { get; private set; }

    private static readonly HashSet<string> ValidPhysicalScale = new HashSet<string>
    {
        "Muy mal",
        "Mal",
        "Regular",
        "Un poco mal",
        "Ni bien ni mal",
        "Un poco bien",
        "Bien",
        "Muy bien",
        "Excelente",
        "Perfecto"
    };

    public PhysicalScale(string name)
    {
        if (!ValidPhysicalScale.Contains(name))
        {
            throw new ArgumentException($"Invalid physical scale: {name}. Valid physical scale are: {string.Join(", ", ValidPhysicalScale)}", nameof(name));
        }

        Name = name;
    }
    public static IEnumerable<string> GetPhysicalScale() => ValidPhysicalScale;

}