using AutoMapper;
using UserManagement.API.Application.Commands.PreferredProfessionalCommands.UpdatePreferredProfessional;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class ProfessionalUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<ProfessionalUpdatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<ProfessionalUpdatedIntegrationEvent>
{
    public async Task Handle(ProfessionalUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<UpdatePreferredProfessionalCommand>(@event);

        await mediator.Send(command);
    }
}
