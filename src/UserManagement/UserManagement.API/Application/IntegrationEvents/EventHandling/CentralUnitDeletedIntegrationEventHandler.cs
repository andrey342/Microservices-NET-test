using UserManagement.API.Application.Commands.CentralUnitCommands.DeleteCentralUnit;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class CentralUnitDeletedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<CentralUnitDeletedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<CentralUnitDeletedIntegrationEvent>
{
    public async Task Handle(CentralUnitDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeleteCentralUnitCommand>(@event);

        await mediator.Send(command);
    }
}

