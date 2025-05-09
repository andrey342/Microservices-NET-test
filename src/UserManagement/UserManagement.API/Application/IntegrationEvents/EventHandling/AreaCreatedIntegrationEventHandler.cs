using UserManagement.API.Application.Commands.AreaCommands.CreateArea;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class AreaCreatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<AreaCreatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<AreaCreatedIntegrationEvent>
{
    public async Task Handle(AreaCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<CreateAreaCommand>(@event);

        await mediator.Send(command);
    }
}