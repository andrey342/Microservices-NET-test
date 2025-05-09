namespace UserManagement.API.Application.Commands.IdentificationCommands.CreateIdentification;
public class CreateIdentificationRequest
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public DateTime? ExpirationDate { get; set; } = null;
    public DateTime? UpdateDate { get; set; } = null;
    public Guid TypeId { get; set; }
}
