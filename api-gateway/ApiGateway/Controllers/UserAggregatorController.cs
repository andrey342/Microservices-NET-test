using ApiGateway.Models.Dtos;
using ApiGateway.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    /// <summary>
    /// Agrega datos de múltiples microservicios y devuelve una única respuesta combinada.
    /// </summary>
    [Route("user")]
    public class UserAggregatorController : BaseController
    {
        public UserAggregatorController(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }


    }
}
