using UserManagement.API.Application.Commands.ResourceCommands.CreateResource;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class ResourceCreatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<ResourceCreatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<ResourceCreatedIntegrationEvent>
{
    public async Task Handle(ResourceCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<CreateResourceCommand>(@event);

        await mediator.Send(command);
    }
}