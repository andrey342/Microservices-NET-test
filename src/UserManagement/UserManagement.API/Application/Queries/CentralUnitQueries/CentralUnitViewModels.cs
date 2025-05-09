namespace UserManagement.API.Application.Queries.CentralUnitQueries;

public record CentralUnitViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string SerialNumber { get; set; }
    public string Phone { get; set; }
}

