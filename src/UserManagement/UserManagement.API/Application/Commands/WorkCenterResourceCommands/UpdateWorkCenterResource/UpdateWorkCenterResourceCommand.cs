namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.UpdateWorkCenterResource;

public class UpdateWorkCenterResourceCommand : IRequest<Result<Unit>>
{
    public UpdateWorkCenterResourceRequest Request { get; set; }

    public UpdateWorkCenterResourceCommand(UpdateWorkCenterResourceRequest request)
    {
        Request = request;
    }
}