using UserManagement.API.Application.Commands.AreaCommands.DeleteArea;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class AreaDeletedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<AreaDeletedIntegrationEventHandler> logger) :
IIntegrationEventHandler<AreaDeletedIntegrationEvent>
{
    public async Task Handle(AreaDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeleteAreaCommand>(@event);

        await mediator.Send(command);
    }
}