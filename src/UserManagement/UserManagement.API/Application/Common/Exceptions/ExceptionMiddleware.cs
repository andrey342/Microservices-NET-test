using System.Net;
using System.Text.Json;
using UserManagement.API.Application.Common.Exceptions.Responses;

namespace UserManagement.API.Application.Common.Exceptions;

/// <summary>
/// Middleware global para el manejo de excepciones en la aplicación.
/// Se encarga de capturar errores, formatear respuestas de error y evitar fugas de información sensible.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Constructor del middleware de excepciones.
    /// </summary>
    /// <param name="next">Delegate para la siguiente ejecución en el pipeline.</param>
    /// <param name="logger">Logger para registrar errores.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Método principal del middleware. Captura excepciones y las maneja de manera centralizada.
    /// </summary>
    /// <param name="httpContext">Contexto HTTP de la solicitud.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); // Continúa con la ejecución de la request
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Maneja las excepciones capturadas y genera una respuesta HTTP formateada.
    /// </summary>
    /// <param name="context">Contexto HTTP de la solicitud.</param>
    /// <param name="exception">Excepción capturada.</param>
    /// <returns>Tarea asincrónica que envía la respuesta HTTP con el error formateado.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        object response = exception switch
        {
            ValidationException validationException => new ValidationExceptionResponse(validationException),
            // Add exception handling for the switch
            //UnauthorizedException unauthorizedException => new UnauthorizedExceptionResponse(unauthorizedException),
            // Default case
            _ => new GeneralExceptionResponse(exception)
        };

        context.Response.StatusCode = response switch
        {
            ValidationExceptionResponse => (int)HttpStatusCode.BadRequest,
            // Add exception handling for the switch
            //UnauthorizedExceptionResponse => (int)HttpStatusCode.Unauthorized,
            // Default case
            _ => (int)HttpStatusCode.InternalServerError
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            WriteIndented = true // Hace que el JSON sea más legible en Swagger y respuestas HTTP
        }));
    }
}