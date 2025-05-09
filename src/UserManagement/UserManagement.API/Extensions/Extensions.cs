using EventBusKafka;
using UserManagement.API.Application.IntegrationEvents.EventHandling;
using UserManagement.API.Application.IntegrationEvents.Events;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.AddKafkaEventBus()
            .AddEventBusSubscriptions();

        // Configurar KafkaEventBus
        services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBus"));
    }

    private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
        eventBus.AddSubscription<WorkCenterCreatedIntegrationEvent, WorkCenterCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<WorkCenterUpdatedIntegrationEvent, WorkCenterUpdatedIntegrationEventHandler>();
        
        eventBus.AddSubscription<ProfessionalUpdatedIntegrationEvent, ProfessionalUpdatedIntegrationEventHandler>();
        
        eventBus.AddSubscription<UserTypeCreatedIntegrationEvent, UserTypeCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<UserTypeUpdatedIntegrationEvent, UserTypeUpdatedIntegrationEventHandler>();
        eventBus.AddSubscription<UserTypeDeletedIntegrationEvent, UserTypeDeletedIntegrationEventHandler>();

        eventBus.AddSubscription<UserTypologyCreatedIntegrationEvent, UserTypologyCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<UserTypologyUpdatedIntegrationEvent, UserTypologyUpdatedIntegrationEventHandler>();
        eventBus.AddSubscription<UserTypologyDeletedIntegrationEvent, UserTypologyDeletedIntegrationEventHandler>();

        eventBus.AddSubscription<AreaCreatedIntegrationEvent, AreaCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<AreaUpdatedIntegrationEvent, AreaUpdatedIntegrationEventHandler>();
        eventBus.AddSubscription<AreaDeletedIntegrationEvent, AreaDeletedIntegrationEventHandler>();

        eventBus.AddSubscription<AreaLevelCreatedIntegrationEvent, AreaLevelCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<AreaLevelUpdatedIntegrationEvent, AreaLevelUpdatedIntegrationEventHandler>();
        eventBus.AddSubscription<AreaLevelDeletedIntegrationEvent, AreaLevelDeletedIntegrationEventHandler>();

        eventBus.AddSubscription<ResourceCreatedIntegrationEvent, ResourceCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<ResourceUpdatedIntegrationEvent, ResourceUpdatedIntegrationEventHandler>();
        eventBus.AddSubscription<ResourceDeletedIntegrationEvent, ResourceDeletedIntegrationEventHandler>();

        eventBus.AddSubscription<CentralUnitCreatedIntegrationEvent, CentralUnitCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<CentralUnitDeletedIntegrationEvent, CentralUnitDeletedIntegrationEventHandler>();
        eventBus.AddSubscription<CentralUnitUpdatedIntegrationEvent, CentralUnitUpdatedIntegrationEventHandler>();

        eventBus.AddSubscription<PeripheralCreatedIntegrationEvent, PeripheralCreatedIntegrationEventHandler>();
        eventBus.AddSubscription<PeripheralDeletedIntegrationEvent, PeripheralDeletedIntegrationEventHandler>();
        eventBus.AddSubscription<PeripheralUpdatedIntegrationEvent, PeripheralUpdatedIntegrationEventHandler>();
    }
}
