using UserManagement.API.Application.IntegrationEvents;

namespace UserManagement.API.Application.Behaviors;

// Difernecias entre UnitOfWork y este behaviour:
// UnitOfWork: Controla las transacciones dentro de un solo contexto/repo.
// --------------
// TransactionBehavior: Controla las transacciones a nivel de aplicación/pipeline,
// incluyendo lógica de negocio y coordinación entre múltiples repositorios o acciones adicionales.
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly UserContext _dbContext;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IUserIntegrationEventService _userIntegrationEventService;

    public TransactionBehavior(UserContext dbContext, IUserIntegrationEventService userIntegrationEventService, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userIntegrationEventService = userIntegrationEventService ?? throw new ArgumentException(nameof(userIntegrationEventService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await _dbContext.BeginTransactionAsync();
                using (_logger.BeginScope(new Dictionary<string, object> { ["TransactionId"] = transaction.TransactionId }))
                {
                    _logger.LogInformation("Beginning transaction {TransactionId} for {Request}", transaction.TransactionId, typeof(TRequest).Name);

                    response = await next();

                    _logger.LogInformation("Committing transaction {TransactionId} for {Request}", transaction.TransactionId, typeof(TRequest).Name);

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                // Lanzar eventos de integracion que se tienen que ejecutar despues de guardar los cambios en BD!!
                await _userIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });

            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Handling transaction for {TransactionId}", request);

            throw;
        }
    }
}
