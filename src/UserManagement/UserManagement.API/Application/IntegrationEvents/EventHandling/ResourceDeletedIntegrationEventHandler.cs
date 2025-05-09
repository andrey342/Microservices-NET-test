using UserManagement.API.Application.Commands.ResourceCommands.DeleteResource;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class ResourceDeletedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<ResourceDeletedIntegrationEventHandler> logger) :
IIntegrationEventHandler<ResourceDeletedIntegrationEvent>
{
    public async Task Handle(ResourceDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeleteResourceCommand>(@event);

        await mediator.Send(command);
    }
}