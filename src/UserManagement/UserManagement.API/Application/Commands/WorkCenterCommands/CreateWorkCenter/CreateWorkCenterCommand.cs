using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.WorkCenterCommands.CreateWorkCenter;

[DataContract]
public class CreateWorkCenterCommand : IRequest<Result<WorkCenterUmViewModel>>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public CreateWorkCenterCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
