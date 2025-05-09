using UserManagement.API.Application.Commands.PeripheralCommands.CreatePeripheral;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class PeripheralCreatedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<PeripheralCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<PeripheralCreatedIntegrationEvent>
{
    public async Task Handle(PeripheralCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var request = mapper.Map<CreatePeripheralRequest>(@event);

        await mediator.Send(new CreatePeripheralCommand(request));
    }
}
