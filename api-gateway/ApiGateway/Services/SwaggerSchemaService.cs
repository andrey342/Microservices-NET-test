using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Polly;
using Polly.Extensions.Http;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ApiGateway.Services
{
    /// <summary>
    /// Servicio encargado de obtener y almacenar en caché los Swagger de los microservicios.
    /// </summary>
    public class SwaggerSchemaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SwaggerSchemaService> _logger;
        private ConcurrentDictionary<string, OpenApiDocument> _cachedSwaggerDocs = new();
        private readonly SemaphoreSlim _lock = new(1, 1);
        private bool _initialized = false;

        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        /// <summary>
        /// Constructor que configura la política de reintento y obtiene las dependencias necesarias.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP utilizado para obtener los Swagger.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <param name="logger">Logger para registrar eventos.</param>
        public SwaggerSchemaService(HttpClient httpClient, IConfiguration configuration, ILogger<SwaggerSchemaService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            // Configuración de Polly: reintento exponencial sin interrumpir el flujo del backend
            _retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()  // Maneja códigos de error HTTP 5xx y 408
                .Or<TaskCanceledException>() // Maneja timeouts
                .OrResult(response => !response.IsSuccessStatusCode) // Captura respuestas HTTP fallidas sin lanzar excepción
                .WaitAndRetryAsync(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2^n segundos (exponencial)
                    (result, timespan, retryAttempt, context) =>
                    {
                        var url = context.TryGetValue("url", out var urlObj) ? urlObj.ToString() : "desconocida";
                        _logger.LogWarning("SwaggerSchemaService: Reintento {RetryAttempt} para {Url}: Esperando {Timespan} segundos. Código HTTP: {StatusCode}",
                            retryAttempt, url, timespan.TotalSeconds, result?.Result?.StatusCode);
                    });
        }

        /// <summary>
        /// Inicializa la carga de Swagger de microservicios en segundo plano.
        /// </summary>
        public async Task InitializeAsync()
        {
            if (_initialized) return;

            _ = Task.Run(async () =>
            {
                await _lock.WaitAsync();
                try
                {
                    if (!_initialized)
                    {
                        var stopwatch = Stopwatch.StartNew();
                        _logger.LogInformation("SwaggerSchemaService: Cargando Swagger de microservicios en segundo plano...");

                        var swaggerDocs = await GetMicroserviceSwaggersAsync();

                        stopwatch.Stop();

                        _cachedSwaggerDocs = new ConcurrentDictionary<string, OpenApiDocument>(swaggerDocs);
                        _initialized = true;

                        _logger.LogInformation("SwaggerSchemaService: Swagger cargado correctamente en {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "SwaggerSchemaService: Error en la inicialización de Swagger.");
                }
                finally
                {
                    _lock.Release();
                }
            });
        }

        /// <summary>
        /// Obtiene los Swagger almacenados en caché.
        /// </summary>
        /// <returns>Diccionario con los Swagger de cada microservicio.</returns>
        public Dictionary<string, OpenApiDocument> GetCachedSwaggerDocs()
        {
            if (!_initialized)
            {
                _logger.LogWarning("SwaggerSchemaService: Advertencia: Swagger aún no está inicializado.");
            }
            return new Dictionary<string, OpenApiDocument>(_cachedSwaggerDocs);
        }

        /// <summary>
        /// Obtiene y fusiona los Swagger de todos los microservicios en paralelo sin detener la ejecución.
        /// </summary>
        private async Task<Dictionary<string, OpenApiDocument>> GetMicroserviceSwaggersAsync()
        {
            var clusters = _configuration.GetSection("ReverseProxy:Clusters").GetChildren();
            var swaggerDocs = new ConcurrentDictionary<string, OpenApiDocument>();

            var tasks = clusters.Select(cluster => Task.Run(() => FetchClusterSwaggerDocsAsync(cluster, swaggerDocs))).ToList();

            await Task.WhenAll(tasks); // Ejecutar todas las tareas en paralelo

            _logger.LogInformation("SwaggerSchemaService: Total de APIs de Swagger cargadas: {Count}", swaggerDocs.Count);
            return new Dictionary<string, OpenApiDocument>(swaggerDocs);
        }

        /// <summary>
        /// Obtiene los Swagger de un cluster de microservicios y los almacena en el diccionario.
        /// </summary>
        private async Task FetchClusterSwaggerDocsAsync(IConfigurationSection cluster, ConcurrentDictionary<string, OpenApiDocument> swaggerDocs)
        {
            var destinations = cluster.GetSection("Destinations").GetChildren();

            foreach (var destination in destinations)
            {
                var baseUrl = destination.GetSection("Address").Value?.TrimEnd('/');
                var swaggerPath = destination.GetSection("SwaggerPath").Value;

                if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(swaggerPath)) continue;

                var fullSwaggerUrl = $"{baseUrl}{swaggerPath}";

                try
                {
                    var document = await FetchSwaggerDocumentAsync(fullSwaggerUrl);
                    if (document != null && !swaggerDocs.ContainsKey(cluster.Key))
                    {
                        swaggerDocs.TryAdd(cluster.Key, document);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Descarga y analiza un documento Swagger desde una URL utilizando política de reintentos.
        /// </summary>
        /// <param name="url">URL del Swagger a obtener.</param>
        /// <returns>Documento OpenAPI si la respuesta es válida, de lo contrario null.</returns>
        private async Task<OpenApiDocument?> FetchSwaggerDocumentAsync(string url)
        {
            var response = await _retryPolicy.ExecuteAsync(async context =>
            {
                try
                {
                    var httpResponse = await _httpClient.GetAsync(url);
                    return httpResponse;
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
                }
            }, new Context { { "url", url } });

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return new OpenApiStringReader().Read(responseBody, out var diagnostic);
        }
    }
}
