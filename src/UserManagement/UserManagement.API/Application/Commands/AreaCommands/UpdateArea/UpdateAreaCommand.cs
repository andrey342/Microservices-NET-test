namespace UserManagement.API.Application.Commands.AreaCommands.UpdateArea;

public class UpdateAreaCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AreaLevelId { get; set; }
    public Guid? ParentId { get; set; }
    public UpdateAreaCommand() { }

    public UpdateAreaCommand(UpdateAreaCommand updateAreaCommand)
    {
        Id = updateAreaCommand.Id;
        AreaLevelId = updateAreaCommand.AreaLevelId;
        Name = updateAreaCommand.Name;
        ParentId = updateAreaCommand.ParentId;
    }
}
