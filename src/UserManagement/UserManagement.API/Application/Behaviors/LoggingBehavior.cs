namespace UserManagement.API.Application.Behaviors;

// Este behaviour registra información antes y después de que se ejecute un handler. Es útil para depuración, auditorías y monitoreo.
#region Como funciona
// 1. Antes del Handler:
// Registra el tipo de request que se está procesando(por ejemplo, CreateUserCommand) y los datos que contiene.
// 2. Después del Handler:
// Registra el tipo de request y la respuesta generada por el handler.
// 3. No Afecta la Lógica del Handler:
// El LoggingBehavior simplemente observa el request y la respuesta, pero no modifica el flujo.
#endregion
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Antes de ejecutar el handler
        _logger.LogInformation($"Handling {typeof(TRequest).Name} with data: {request}");

        var response = await next();

        // Después de ejecutar el handler
        _logger.LogInformation($"Handled {typeof(TRequest).Name} with response: {response}");

        return response;
    }
}

