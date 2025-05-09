namespace UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;

public class CreatePersonalResourceRequest
{
    public string Name { get; set; }
    public string? Observations { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public Guid UserId { get; set; }
}