using UserManagement.API.Application.Commands.PeripheralCommands.UpdatePeripheral;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class PeripheralUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<PeripheralUpdatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<PeripheralUpdatedIntegrationEvent>
{
    public async Task Handle(PeripheralUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var request = mapper.Map<UpdatePeripheralRequest>(@event);

        await mediator.Send(new UpdatePeripheralCommand(request));
    }
}
