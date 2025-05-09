using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.Commands.ResourceCommands.UpdateResource;

public class UpdateResourceCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public PhoneNumbers PhoneNumbers { get; private set; }
    public Guid WorkCenterId { get; set; }

    public UpdateResourceCommand() { }

    public UpdateResourceCommand(Guid id, string name, PhoneNumbers phoneNumbers, Guid workCenterId)
    {
        Id = id;
        Name = name;
        PhoneNumbers = phoneNumbers;
        WorkCenterId = workCenterId;
    }
}