using ApiGateway.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiGateway.Swagger
{
    /// <summary>
    /// Filtro para agregar dinámicamente las rutas de los microservicios al Swagger del API Gateway,
    /// basado en la configuración de YARP.
    /// </summary>
    public class YarpSwaggerDocumentFilter : IDocumentFilter
    {
        private readonly IConfiguration _configuration;
        private readonly SwaggerSchemaService _swaggerSchemaService;
        private readonly ILogger<YarpSwaggerDocumentFilter> _logger;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <param name="swaggerSchemaService">Servicio que contiene los Swagger de los microservicios.</param>
        /// <param name="logger">Logger para registrar eventos.</param>
        public YarpSwaggerDocumentFilter(IConfiguration configuration, SwaggerSchemaService swaggerSchemaService, ILogger<YarpSwaggerDocumentFilter> logger)
        {
            _configuration = configuration;
            _swaggerSchemaService = swaggerSchemaService;
            _logger = logger;
        }

        /// <summary>
        /// Aplica el filtro al documento Swagger del API Gateway, añadiendo las rutas de los microservicios registrados en YARP.
        /// </summary>
        /// <param name="swaggerDoc">Documento OpenAPI de la API Gateway.</param>
        /// <param name="context">Contexto de generación de Swagger.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var routes = _configuration.GetSection("ReverseProxy:Routes").GetChildren();
            var microserviceSwaggers = _swaggerSchemaService.GetCachedSwaggerDocs();

            if (microserviceSwaggers.Count == 0)
            {
                _logger.LogWarning("Advertencia: Swagger aún no ha sido inicializado.");
                return;
            }

            foreach (var route in routes)
            {
                ProcessRoute(route, microserviceSwaggers, swaggerDoc);
            }
        }

        /// <summary>
        /// Procesa una ruta específica de YARP y la añade al Swagger del API Gateway si es válida.
        /// </summary>
        /// <param name="route">Configuración de la ruta en YARP.</param>
        /// <param name="microserviceSwaggers">Diccionario con los Swagger de los microservicios.</param>
        /// <param name="swaggerDoc">Documento OpenAPI del API Gateway.</param>
        private void ProcessRoute(IConfigurationSection route, Dictionary<string, OpenApiDocument> microserviceSwaggers, OpenApiDocument swaggerDoc)
        {
            var path = route.GetSection("Match:Path").Value;
            var clusterId = route.GetSection("ClusterId").Value;
            var label = route.GetSection("Label").Value;

            if (string.IsNullOrEmpty(path) || path.Contains("{**catch-all}") || string.IsNullOrEmpty(label) || string.IsNullOrEmpty(clusterId))
                return;

            if (!microserviceSwaggers.TryGetValue(clusterId, out var microserviceSwagger) || !microserviceSwagger.Paths.ContainsKey(path))
                return;

            var originalPathItem = microserviceSwagger.Paths[path];

            if (!swaggerDoc.Paths.ContainsKey(path))
            {
                swaggerDoc.Paths[path] = originalPathItem;
            }

            foreach (var operation in originalPathItem.Operations.Values)
            {
                operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = label } };
            }

            CopySchemas(microserviceSwagger, swaggerDoc);
        }

        /// <summary>
        /// Copia los esquemas de un Swagger de un microservicio al Swagger del API Gateway.
        /// </summary>
        /// <param name="microserviceSwagger">Swagger del microservicio.</param>
        /// <param name="swaggerDoc">Documento OpenAPI del API Gateway.</param>
        private void CopySchemas(OpenApiDocument microserviceSwagger, OpenApiDocument swaggerDoc)
        {
            foreach (var schema in microserviceSwagger.Components.Schemas)
            {
                if (!swaggerDoc.Components.Schemas.ContainsKey(schema.Key))
                {
                    swaggerDoc.Components.Schemas.Add(schema.Key, schema.Value);
                }
            }
        }
    }
}