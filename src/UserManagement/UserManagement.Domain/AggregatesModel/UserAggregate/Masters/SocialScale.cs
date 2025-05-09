namespace UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

public record SocialScale
{
    public string Name { get; private set; }

    private static readonly HashSet<string> ValidSocialScale = new HashSet<string>
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

    public SocialScale(string name)
    {
        if (!ValidSocialScale.Contains(name))
        {
            throw new ArgumentException($"Invalid social scale: {name}. Valid social scale are: {string.Join(", ", ValidSocialScale)}", nameof(name));
        }

        Name = name;
    }
    public static IEnumerable<string> GetSocialScale() => ValidSocialScale;

}