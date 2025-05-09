namespace UserManagement.API.Application.Commands.UserTypeCommands.CreateUserType;

public class CreateUserTypeCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkCenterId { get; set; }

    public CreateUserTypeCommand() { }

    public CreateUserTypeCommand(Guid id, string name, Guid workCenterId)
    {
        Id = id;
        WorkCenterId = workCenterId;
        Name = name;
    }
}

