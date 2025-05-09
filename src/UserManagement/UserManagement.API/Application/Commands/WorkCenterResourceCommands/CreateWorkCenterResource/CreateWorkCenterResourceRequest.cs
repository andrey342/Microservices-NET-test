namespace UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;

public class CreateWorkCenterResourceRequest
{
    public string? Observations { get; set; }
    public Guid UserId { get; set; }
    public Guid ResourceId { get; set; }
}
