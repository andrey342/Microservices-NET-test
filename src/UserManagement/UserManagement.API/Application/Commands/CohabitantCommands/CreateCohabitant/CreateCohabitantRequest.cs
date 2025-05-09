namespace UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;

public class CreateCohabitantRequest
{
    public Guid ResidenceId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname1 { get; set; } = null!;
    public string? Surname2 { get; set; }
    public string? Mobile { get; set; }
    public string? Observation { get; set; }
    public bool Resource { get; set; }
    public Guid CohabitantTypeId { get; set; }
}