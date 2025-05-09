using UserManagement.API.Application.Commands.AreaCommands.UpdateArea;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class AreaUpdatedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<AreaUpdatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<AreaUpdatedIntegrationEvent>
{
    public async Task Handle(AreaUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<UpdateAreaCommand>(@event);

        await mediator.Send(command);
    }
}