using UserManagement.API.Application.Commands.CentralUnitCommands.UpdateCentralUnit;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class CentralUnitUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<CentralUnitUpdatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<CentralUnitUpdatedIntegrationEvent>
{
    public async Task Handle(CentralUnitUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var request = mapper.Map<UpdateCentralUnitRequest>(@event);

        await mediator.Send(new UpdateCentralUnitCommand(request));
    }
}
