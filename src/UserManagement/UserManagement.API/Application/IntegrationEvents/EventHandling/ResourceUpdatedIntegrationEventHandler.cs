using UserManagement.API.Application.Commands.ResourceCommands.UpdateResource;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class ResourceUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<ResourceCreatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<ResourceUpdatedIntegrationEvent>
{
    public async Task Handle(ResourceUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<UpdateResourceCommand>(@event);

        await mediator.Send(command);
    }
}