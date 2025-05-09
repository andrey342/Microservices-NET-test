using System.Net;

namespace UserManagement.API.Application.Common.Exceptions.Responses;

/// <summary>
/// Representa una respuesta de error para excepciones de validación.
/// </summary>
public class ValidationExceptionResponse
{
    public int Status { get; } = (int)HttpStatusCode.BadRequest;
    public bool Success { get; } = false;
    public string Title { get; } = "Validation Error";
    public Dictionary<string, List<string>> Errors { get; }

    public ValidationExceptionResponse(ValidationException exception)
    {
        Errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).Distinct().ToList() // Evita duplicados
            );
    }
}
