namespace UserManagement.API.Application.Commands.UserTypologyCommands.UpdateUserTypology;

public class UpdateUserTypologyCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkCenterId { get; set; }

    public UpdateUserTypologyCommand() { }

    public UpdateUserTypologyCommand(Guid id, string name, Guid workCenterId)
    {
        Id = id;
        WorkCenterId = workCenterId;
        Name = name;
    }
}
