using Yarp.ReverseProxy.Forwarder;

namespace ApiGateway.Services
{
    /// <summary>
    /// Fábrica personalizada para YARP que envuelve el manejador HTTP con las políticas de resiliencia.
    /// Esto asegura que todas las peticiones sean procesadas con las reglas de Polly antes de salir de la API Gateway.
    /// </summary>
    public class CustomHttpClientFactory : ForwarderHttpClientFactory
    {
        private readonly PolicyHandler _policyHandler;

        /// <summary>
        /// Constructor que recibe el DelegatingHandler con las políticas de Polly.
        /// </summary>
        /// <param name="policyHandler">El manejador con reintento y circuit breaker.</param>
        public CustomHttpClientFactory(PolicyHandler policyHandler)
        {
            _policyHandler = policyHandler;
        }

        /// <summary>
        /// Método que envuelve cada solicitud HTTP con el `PolicyHandler` antes de ser enviada a su destino final.
        /// </summary>
        /// <param name="context">Contexto del cliente HTTP en YARP.</param>
        /// <param name="handler">El `HttpMessageHandler` original de YARP.</param>
        /// <returns>El `HttpMessageHandler` modificado con las políticas de resiliencia.</returns>
        protected override HttpMessageHandler WrapHandler(ForwarderHttpClientContext context, HttpMessageHandler handler)
        {
            _policyHandler.InnerHandler = handler;
            return _policyHandler;
        }
    }

}
