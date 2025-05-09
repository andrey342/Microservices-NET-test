using UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;

namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.UpdateWorkCenterResource;

public class UpdateWorkCenterResourceRequest : CreateWorkCenterResourceRequest
{
    public Guid Id { get; set; }
}