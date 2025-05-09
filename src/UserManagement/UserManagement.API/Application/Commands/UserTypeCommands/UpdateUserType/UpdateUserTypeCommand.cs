namespace UserManagement.API.Application.Commands.UserTypeCommands.UpdateUserType;

public class UpdateUserTypeCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkCenterId { get; set; }

    public UpdateUserTypeCommand() { }

    public UpdateUserTypeCommand(Guid id, string name, Guid workCenterId)
    {
        Id = id;
        WorkCenterId = workCenterId;
        Name = name;
    }
}
