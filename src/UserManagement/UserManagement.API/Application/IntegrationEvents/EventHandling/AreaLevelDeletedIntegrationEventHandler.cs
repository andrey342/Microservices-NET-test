using UserManagement.API.Application.Commands.AreaLevelCommands.DeleteAreaLevel;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class AreaLevelDeletedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<AreaLevelDeletedIntegrationEventHandler> logger) :
IIntegrationEventHandler<AreaLevelDeletedIntegrationEvent>
{
    public async Task Handle(AreaLevelDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeleteAreaLevelCommand>(@event);

        await mediator.Send(command);
    }
}