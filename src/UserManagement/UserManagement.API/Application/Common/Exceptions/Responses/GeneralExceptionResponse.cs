using System.Net;

namespace UserManagement.API.Application.Common.Exceptions.Responses;

/// <summary>
/// Representa una respuesta de error para excepciones generales.
/// </summary>
public class GeneralExceptionResponse
{
    public int Status { get; } = (int)HttpStatusCode.InternalServerError;
    public bool Success { get; } = false;
    public string Title { get; } = "Internal Server Error";
    public string Detail { get; }
    public string InnerDetail { get; } = null;

    public GeneralExceptionResponse(Exception exception)
    {
        Detail = exception.Message;
        InnerDetail = exception.InnerException?.Message;
    }
}
