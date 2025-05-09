using ApiGateway.Services;
using ApiGateway.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System.Threading.RateLimiting;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

// Configurar el sistema de logging
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();

#region HealthCheck
builder.Services.AddHealthChecks();
#endregion

#region Autenticación con Azure AD y validación de tokens

if (!isDevelopment)
{
    #region Autenticación con Azure AD y validación de tokens

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["AzureAd:Authority"];
            options.Audience = builder.Configuration["AzureAd:Audience"];
            // Parámetros de validación del token recibido
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,           // Verifica que el emisor del token es válido
                ValidateAudience = true,         // Verifica que el token es para este API Gateway
                ValidateLifetime = true,         // Verifica que el token no ha expirado
                ValidateIssuerSigningKey = true  // Verifica la firma del token para evitar manipulaciones
            };
        });

    #endregion

    #region Configurar Autorización y validación de roles/claims

    // Configura la autorización para restringir el acceso según roles de usuario.
    // Se crean dos políticas: 
    // - "RequireAdmin": Solo los usuarios con el rol "Admin" pueden acceder.
    // - "RequireUser": Solo los usuarios con el rol "User" pueden acceder.
    builder.Services.AddAuthorization(options =>
    {
        //options.AddPolicy("RequireAdmin", policy => policy.RequireClaim("roles", "Admin"));
        //options.AddPolicy("RequireUser", policy => policy.RequireClaim("roles", "User"));
        options.AddPolicy("RequireAuth", policy => policy.RequireAuthenticatedUser()); // Solo usuarios autenticados pueden acceder
    });

    #endregion
}
else
{
    logger.LogInformation("Modo Development: Autenticación DESACTIVADA");
}

#endregion

#region Configurar Rate Limiting (500 peticiones por usuario/IP cada minuto)

// Implementa un sistema de rate limiting (limitador de peticiones) usando la IP o el usuario autenticado.
// Esto evita que un solo usuario haga demasiadas peticiones y sobrecargue el API Gateway.
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        // Determina el identificador único para la limitación: usa el nombre del usuario autenticado o "anonymous" si no está autenticado.
        var user = context.User.Identity?.Name ?? "anonymous";
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: user, // Cada usuario/IP tendrá su propio límite
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 500, // 500 requests por minuto
                Window = TimeSpan.FromMinutes(1) // Cada 1 minuto se reinicia el contador
            });
    });
});

#endregion

#region Configurar Circuit Breaker & Retry Policy (Libreria Polly)
// Configura políticas de resiliencia con Polly para manejar errores en los microservicios.
// Estas políticas se aplicarán a todas las solicitudes que pasen por la API Gateway.

// Politica de Reintento:
// Si una petición falla por un error transitorio (Ej: timeout o error 500), se intentará de nuevo hasta 3 veces.
// El tiempo de espera entre intentos sigue una progresión exponencial (2^intento segundos).
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        onRetryAsync: (outcome, timespan, retryAttempt, context) =>
        {
            logger.LogWarning("Reintento {RetryAttempt}: Esperando {Timespan} segundos antes del próximo intento.",
                retryAttempt, timespan.TotalSeconds);
            return Task.CompletedTask;
        });

// Circuit Breaker:
// Si los reintentos se ejecutan y fallan 5 veces (1 vez por politica de reintento), se abre el circuito y deja de hacer peticiones por 1 minuto.
// Esto evita seguir sobrecargando un servicio que ya está caído.
var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromMinutes(1),
        onBreak: (exception, timespan) =>
        {
            logger.LogError("Circuit Breaker ACTIVADO por {Timespan} segundos. Error: {ErrorMessage}",
                            timespan.TotalSeconds, exception.Exception.Message);
        },
        onReset: () =>
        {
            logger.LogInformation("Circuit Breaker RESTAURADO: Se reanudan las solicitudes.");
        });

// Envuelve ambas políticas en una sola para que sean aplicadas juntas.
// Primero se ejecuta los reintentos, luego el circuit breaker.
var policyWrap = Policy.WrapAsync(circuitBreakerPolicy, retryPolicy);
// Registra la política de resiliencia en el contenedor de dependencias.
builder.Services.AddSingleton(new PolicyHandler(policyWrap));

#endregion

#region Configurar YARP con Circuit Breaker & Retry Policy

// Configura la API Gateway para que use la fábrica personalizada de clientes HTTP.
builder.Services.AddSingleton<IForwarderHttpClientFactory, CustomHttpClientFactory>();

// Carga la configuración de enrutamiento de YARP desde appsettings.json.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://asisto-tad-pre.clece.es", "https://asisto-tad.clece.es")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});
#endregion

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Configurar Swagger

builder.Services.AddSingleton<SwaggerSchemaService>();
builder.Services.AddHostedService<SwaggerSchemaInitializer>();

builder.Services.AddTransient<YarpSwaggerDocumentFilter>();

builder.Services.AddSwaggerGen(c =>
{
    // Luego cargamos dinámicamente las rutas desde YARP y los microservicios
    c.DocumentFilter<YarpSwaggerDocumentFilter>();
});
#endregion

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.MapReverseProxy();

app.UseCors("AllowSpecificOrigins");

if (!isDevelopment)
{
    app.UseAuthentication();
    app.UseAuthorization();
    // Definir que la API Gateway debe requerir autenticación antes de enrutar peticiones
    app.MapReverseProxy().RequireAuthorization("RequireAuth");
}
else
{
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1"));
}

app.UseRateLimiter();

// Middleware de Logs de Peticiones
app.Use(async (context, next) =>
{
    logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
    await next();
    logger.LogInformation($"Response: {context.Response.StatusCode}");
});

// Middleware para Soporte de WebSockets
app.UseWebSockets();
app.MapControllers();

#region Configuración de HealthCheck

app.UseHealthChecks("/healthz");

#endregion

app.Run();