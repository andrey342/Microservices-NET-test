using UserManagement.API.Application.Commands.AreaLevelCommands.CreateAreaLevel;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;
public class AreaLevelCreatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<AreaLevelCreatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<AreaLevelCreatedIntegrationEvent>
{
    public async Task Handle(AreaLevelCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<CreateAreaLevelCommand>(@event);

        await mediator.Send(command);
    }
}