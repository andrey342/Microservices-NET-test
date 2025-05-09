using UserManagement.API.Application.Behaviors;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Repositories;
using UserManagement.API.Application.Services;
using Asp.Versioning;
using UserManagement.Infrastructure.Idempotency;
using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using IntegrationEventLog.Services;
using UserManagement.API.Application.IntegrationEvents;
using UserManagement.API.Application.IntegrationEvents.EventHandling;
using UserManagement.API.Application.IntegrationEvents.Events;
using UserManagement.API.Application.Common.Exceptions;
using UserManagement.API.Application.Commands.IdentifiedCommands;
using UserManagement.API.Apis;
using UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract;
using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.API.Application.Queries.ServiceContractQueries;
using UserManagement.API.Application.Commands.CohabitantCommands.CreateCohabitant;
using UserManagement.API.Application.Queries.WorkCenterQueries;
using UserManagement.API.Application.Commands.ResidenceCommands.CreateResidence;
using UserManagement.API.Application.Commands.KeyCommands.CreateKey;
using UserManagement.API.Application.Commands.MedicalInformationCommands.CreateMedicalInformation;
using UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;
using UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;
using UserManagement.API.Application.Commands.HealthCoverageCommands.CreateHealthCoverage;
using UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;
using UserManagement.API.Application.Commands.WorkCenterResourceCommands.CreateWorkCenterResource;
using UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;

var builder = WebApplication.CreateBuilder(args);

#region HealthCheck
builder.Services.AddHealthChecks();
#endregion

#region Configuracion de la base de datos

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                        builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("UserManagement.Infrastructure")));

#endregion

#region Configuracion de servicios

#region Configuración de Servicios de Integración

builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<UserContext>>();
builder.Services.AddTransient<IUserIntegrationEventService, UserIntegrationEventService>();

#endregion

#region Configuración de MediatR y Handlers

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

//Configurar Integration Event
builder.Services.AddTransient<IIntegrationEventHandler<WorkCenterCreatedIntegrationEvent>, WorkCenterCreatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<ProfessionalUpdatedIntegrationEvent>, ProfessionalUpdatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<UserTypeCreatedIntegrationEvent>, UserTypeCreatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<UserTypeUpdatedIntegrationEvent>, UserTypeUpdatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<UserTypeDeletedIntegrationEvent>, UserTypeDeletedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<UserTypologyCreatedIntegrationEvent>, UserTypologyCreatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<UserTypologyUpdatedIntegrationEvent>, UserTypologyUpdatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<UserTypologyDeletedIntegrationEvent>, UserTypologyDeletedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<AreaCreatedIntegrationEvent>, AreaCreatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<AreaUpdatedIntegrationEvent>, AreaUpdatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<AreaDeletedIntegrationEvent>, AreaDeletedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<AreaLevelCreatedIntegrationEvent>, AreaLevelCreatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<AreaLevelUpdatedIntegrationEvent>, AreaLevelUpdatedIntegrationEventHandler>();
builder.Services.AddTransient<IIntegrationEventHandler<AreaLevelDeletedIntegrationEvent>, AreaLevelDeletedIntegrationEventHandler>();

// Configuracion del FluentValidator para todos los validators del proyecto
// Registrar los handlers de idempotencia
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateUserCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateUserCommand, Result<Guid>>
    >();

builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateServiceContractCommand, Result<CreatedContractViewModel>>, Result<CreatedContractViewModel>>,
    IdentifiedCommandHandler<CreateServiceContractCommand, Result<CreatedContractViewModel>>
    >();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateCohabitantCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateCohabitantCommand, Result<Guid>>
    >();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateKeyCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateKeyCommand, Result<Guid>>
    >();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateResidenceCommand, Result<ResidenceViewModel>>, Result<ResidenceViewModel>>,
    IdentifiedCommandHandler<CreateResidenceCommand, Result<ResidenceViewModel>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateMedicalInformationCommand, Result<MedicalInformationViewModel>>, Result<MedicalInformationViewModel>>,
    IdentifiedCommandHandler<CreateMedicalInformationCommand, Result<MedicalInformationViewModel>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateMedicalConditionCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateMedicalConditionCommand, Result<Guid>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateAllergyImpactCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateAllergyImpactCommand, Result<Guid>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateHealthCoverageCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateHealthCoverageCommand, Result<Guid>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateMedicationCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateMedicationCommand, Result<Guid>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreateWorkCenterResourceCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreateWorkCenterResourceCommand, Result<Guid>>
>();
builder.Services.AddScoped<IRequestHandler<
    IdentifiedCommand<CreatePersonalResourceCommand, Result<Guid>>, Result<Guid>>,
    IdentifiedCommandHandler<CreatePersonalResourceCommand, Result<Guid>>
>();

#endregion

#region Configuración de Validaciones y AutoMapper
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

#endregion

#region Inyección de Dependencias (IoC)

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserQueries, UserQueries>();
builder.Services.AddScoped<IServiceContractQueries, ServiceContractQueries>();
builder.Services.AddScoped<IWorkCenterQueries, WorkCenterQueries>();
builder.Services.AddScoped<IServiceContractRepository, ServiceContractRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkCenterRepository, WorkCenterRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<IUserTypologyRepository, UserTypologyRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAreaLevelRepository, AreaLevelRepository>();
builder.Services.AddScoped<ICentralUnitRepository, CentralUnitRepository>();
builder.Services.AddScoped<IPeripheralRepository, PeripheralRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IRequestManager, RequestManager>();

#endregion

#region Registro de Pipelines de Comportamiento (Behaviors)

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

#endregion

#region Configuración de Middleware y Servicios

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RetryFailedIntegrationEventService>();

builder.AddApplicationServices();
#endregion

#region Configuración de API Versioning

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

#endregion

#endregion

var app = builder.Build();

#region Configuracion del pipeline de solicitudes HTTP

#region Configuración de Middlewares

app.UseMiddleware<ExceptionMiddleware>(); // Middleware de manejo de excepciones

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
        c.RoutePrefix = "swagger";
    });
}

#endregion

#region Configuración de Rutas y Endpoints

app.MapDefaultControllerRoute();

app.NewVersionedApi("Users").MapUsersApiV1();
app.NewVersionedApi("ServiceContracts").MapServiceContractsApiV1();
app.NewVersionedApi("WorkCenters").MapWorkCentersApiV1();

#endregion

#region Configuración de HealthCheck

app.UseHealthChecks("/healthz");

#endregion

app.Run();

#endregion