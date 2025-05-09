namespace UserManagement.API.Application.Commands.AreaCommands.CreateArea;

public class CreateAreaCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AreaLevelId { get; set; }
    public Guid? ParentId { get; set; }
    public CreateAreaCommand() { }

    public CreateAreaCommand(CreateAreaCommand createAreaCommand)
    {
        Id = createAreaCommand.Id;
        AreaLevelId = createAreaCommand.AreaLevelId;
        ParentId = createAreaCommand.ParentId;
        Name = createAreaCommand.Name;
    }
}
