using UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class CentralUnitCreatedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<CentralUnitCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<CentralUnitCreatedIntegrationEvent>
{
    public async Task Handle(CentralUnitCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var request = mapper.Map<CreateCentralUnitRequest>(@event);

        await mediator.Send(new CreateCentralUnitCommand(request));
    }
}
