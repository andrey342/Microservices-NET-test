namespace UserManagement.API.Application.Commands.AreaLevelCommands.CreateAreaLevel;

public class CreateAreaLevelCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public Guid WorkCenterId { get; set; }

    public CreateAreaLevelCommand() { }

    public CreateAreaLevelCommand(Guid id, string name, int level, Guid workCenterId)
    {
        Id = id;
        WorkCenterId = workCenterId;
        Name = name;
        Level = level;
    }
}