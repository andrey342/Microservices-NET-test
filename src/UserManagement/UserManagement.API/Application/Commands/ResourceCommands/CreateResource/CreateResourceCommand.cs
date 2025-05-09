using UserManagement.Domain.ValueObjects;

namespace UserManagement.API.Application.Commands.ResourceCommands.CreateResource;

public class CreateResourceCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public PhoneNumbers PhoneNumbers { get; private set; }
    public Guid WorkCenterId { get; set; }

    public CreateResourceCommand() { }

    public CreateResourceCommand(Guid id, string name, PhoneNumbers phoneNumbers, Guid workCenterId)
    {
        Id = id;
        Name = name;
        PhoneNumbers = phoneNumbers;
        WorkCenterId = workCenterId;
    }
}
