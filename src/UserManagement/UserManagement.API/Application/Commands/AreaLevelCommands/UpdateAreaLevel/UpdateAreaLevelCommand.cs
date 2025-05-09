namespace UserManagement.API.Application.Commands.AreaLevelCommands.UpdateAreaLevel;

public class UpdateAreaLevelCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }

    public UpdateAreaLevelCommand() { }

    public UpdateAreaLevelCommand(Guid id, string name, int level)
    {
        Id = id;
        Level = level;
        Name = name;
    }
}
