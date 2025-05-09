namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.DeleteWorkCenterResource;

public class DeleteWorkCenterResourceCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; }

    public DeleteWorkCenterResourceCommand(Guid id)
    {
        Id = id;
    }
}
