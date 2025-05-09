using UserManagement.API.Application.Commands.UserTypologyCommands.UpdateUserTypology;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class UserTypologyUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<UserTypologyUpdatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<UserTypologyUpdatedIntegrationEvent>
{
    public async Task Handle(UserTypologyUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<UpdateUserTypologyCommand>(@event);

        await mediator.Send(command);
    }
}