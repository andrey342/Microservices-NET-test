using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiGateway.Controllers
{
    [ApiController]
    #if !DEBUG // Solo en Producción se aplica automáticamente la autenticación
    [Microsoft.AspNetCore.Authorization.Authorize]
    #endif
    public abstract class BaseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        protected BaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Antes de ejecutar cualquier acción, verifica que la petición proviene de YARP.
        /// Si no es así, devuelve un error 403 (Forbidden).
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var request = context.HttpContext.Request;
            var referer = request.Headers["Referer"].ToString();
            var forwardedFor = request.Headers["X-Forwarded-For"].ToString();

            // Permitir solo peticiones que provengan de la API Gateway (YARP)
            if (string.IsNullOrEmpty(referer) && string.IsNullOrEmpty(forwardedFor))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }

        /// <summary>
        /// Método genérico para hacer peticiones GET a través de YARP con autenticación automática en Producción.
        /// En Desarrollo, ignora la autenticación.
        /// </summary>
        protected async Task<T?> GetFromYarpAsync<T>(string route)
        {
            var httpClient = _httpClientFactory.CreateClient("aggregationService");

            #if !DEBUG
            var accessToken = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(accessToken))
                throw new UnauthorizedAccessException("No se encontró un token de autenticación.");

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.Add("Authorization", accessToken);
            #else
            var request = new HttpRequestMessage(HttpMethod.Get, route);
            #endif

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error en {route}. Código de estado: {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
