namespace UserManagement.API.Application.Common.Models;

/// <summary>
/// Clase base para los ViewModels de Maestros con solo Id y Name.
/// </summary>
public record MasterViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}