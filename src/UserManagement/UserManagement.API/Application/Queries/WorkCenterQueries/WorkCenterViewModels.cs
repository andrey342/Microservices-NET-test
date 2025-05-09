using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Queries.WorkCenterQueries;

public record UserTypeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkCenterId { get; set; }
}

public record UserTypologyViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkCenterId { get; set; }
}

public record AreaLevelViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public Guid WorkCenterId { get; set; }
}

public record AreaViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AreaLevelId { get; set; }
}

public record ResourceBasicViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Mobile { get; set; } = null;
    public string? Phone { get; set; } = null;
    public Guid WorkCenterId { get; set; }
}
