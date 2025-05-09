namespace ApiGateway.Services
{
    /// <summary>
    /// Servicio alojado que inicializa la carga de Swagger en segundo plano al iniciar la aplicación.
    /// </summary>
    public class SwaggerSchemaInitializer : IHostedService
    {
        private readonly SwaggerSchemaService _swaggerSchemaService;

        /// <summary>
        /// Constructor del servicio de inicialización de Swagger.
        /// </summary>
        /// <param name="swaggerSchemaService">Instancia del servicio encargado de cargar los Swagger.</param>
        public SwaggerSchemaInitializer(SwaggerSchemaService swaggerSchemaService)
        {
            _swaggerSchemaService = swaggerSchemaService;
        }

        /// <summary>
        /// Método que se ejecuta al iniciar la aplicación para cargar los Swagger en segundo plano.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación para detener la inicialización.</param>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = _swaggerSchemaService.InitializeAsync(); // Se ejecuta en segundo plano, pero no bloquea el backend
            return Task.CompletedTask;
        }

        /// <summary>
        /// Método que se ejecuta al detener la aplicación. No requiere ninguna acción en este caso.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación para detener la ejecución.</param>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
