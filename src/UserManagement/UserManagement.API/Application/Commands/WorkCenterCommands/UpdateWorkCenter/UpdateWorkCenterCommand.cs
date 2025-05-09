using UserManagement.API.Application.Queries.ServiceContractQueries;

namespace UserManagement.API.Application.Commands.WorkCenterCommands.UpdateWorkCenter;

[DataContract]
public class UpdateWorkCenterCommand : IRequest<Result<WorkCenterUmViewModel>>
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public UpdateWorkCenterCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
