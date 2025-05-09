namespace UserManagement.API.Application.Commands.UserTypologyCommands.CreateUserTypology;

public class CreateUserTypologyCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkCenterId { get; set; }

    public CreateUserTypologyCommand() { }

    public CreateUserTypologyCommand(Guid id, string name, Guid workCenterId)
    {
        Id = id;
        WorkCenterId = workCenterId;
        Name = name;
    }
}