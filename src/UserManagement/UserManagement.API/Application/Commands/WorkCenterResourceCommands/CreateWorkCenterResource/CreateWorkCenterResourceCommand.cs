namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;

public class CreateWorkCenterResourceCommand : IRequest<Result<Guid>>
{
    public CreateWorkCenterResourceRequest Request { get; set; }

    public CreateWorkCenterResourceCommand(CreateWorkCenterResourceRequest request)
    {
        Request = request;
    }
}