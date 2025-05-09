namespace UserManagement.API.Application.Behaviors;

// Este behaviour asegura que todos los comandos y consultas sean validados automáticamente antes de ser procesados por sus handlers.
#region Como funciona
// 1. Recolecta Validadores:
    // Usa los validadores registrados(por ejemplo, los de FluentValidation) que correspondan al tipo de request(TRequest).
// 2. Ejecuta Validaciones:
    // Pasa el request al contexto de validación de FluentValidation.
// Si se encuentran errores, lanza una ValidationException con los detalles.
    // 3. Detiene la Ejecución:
// Si hay errores de validación, el pipeline se detiene y el handler nunca se ejecuta.
    // 4. Continúa si No Hay Errores:
// Si no hay errores, el control pasa al siguiente behaviour o al handler.
#endregion
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            // Ejecuta todos los validadores en paralelo y recolecta errores únicos
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(f => new { f.PropertyName, f.ErrorMessage }) // Evita duplicados
                .Select(g => g.First()) // Obtén solo el primero de cada grupo
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
