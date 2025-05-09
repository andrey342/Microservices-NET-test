using Polly;

namespace ApiGateway.Services
{
    /// <summary>
    /// DelegatingHandler que aplica políticas de reintento y circuit breaker a las solicitudes HTTP.
    /// Se usa en la API Gateway para mejorar la resiliencia ante fallos en los microservicios.
    /// </summary>
    public class PolicyHandler : DelegatingHandler
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _policy;

        /// <summary>
        /// Constructor que recibe una política de Polly y la asigna a este handler.
        /// </summary>
        /// <param name="policy">Política de resiliencia que incluye reintentos y circuit breaker.</param>
        public PolicyHandler(IAsyncPolicy<HttpResponseMessage> policy)
        {
            _policy = policy;
        }

        /// <summary>
        /// Captura cada petición HTTP y aplica la política definida antes de enviarla al destino final.
        /// </summary>
        /// <param name="request">La solicitud HTTP a enviar.</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud si es necesario.</param>
        /// <returns>La respuesta HTTP obtenida después de aplicar la política.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await _policy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }
    }

}
