using UserManagement.API.Application.Commands.AreaLevelCommands.UpdateAreaLevel;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class AreaLevelUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<AreaLevelUpdatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<AreaLevelUpdatedIntegrationEvent>
{
    public async Task Handle(AreaLevelUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<UpdateAreaLevelCommand>(@event);

        await mediator.Send(command);
    }
}