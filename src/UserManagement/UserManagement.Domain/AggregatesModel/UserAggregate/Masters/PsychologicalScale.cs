namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

public record PsychologicalScale
{
    public string Name { get; private set; }

    private static readonly HashSet<string> ValidPsychologicalScale = new HashSet<string>
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

    public PsychologicalScale(string name)
    {
        if (!ValidPsychologicalScale.Contains(name))
        {
            throw new ArgumentException($"Invalid psycological scale: {name}. Valid psychological scales are: {string.Join(", ", ValidPsychologicalScale)}", nameof(name));
        }

        Name = name;
    }
    public static IEnumerable<string> GetPsychologicalScale() => ValidPsychologicalScale;

}