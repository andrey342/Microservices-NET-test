namespace UserManagement.API.Application.Queries.PeripheralQueries;

public record PeripheralViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string SerialNumber { get; set; }
}


