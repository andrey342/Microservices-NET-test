using UserManagement.API.Application.Commands.PeripheralCommands.DeletePeripheral;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class PeripheralDeletedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<PeripheralDeletedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<PeripheralDeletedIntegrationEvent>
{
    public async Task Handle(PeripheralDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeletePeripheralCommand>(@event);

        await mediator.Send(command);
    }
}
