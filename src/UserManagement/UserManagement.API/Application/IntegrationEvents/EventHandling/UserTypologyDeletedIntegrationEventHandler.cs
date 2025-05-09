using UserManagement.API.Application.Commands.UserTypologyCommands.DeleteUserTypology;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class UserTypologyDeletedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<UserTypologyDeletedIntegrationEventHandler> logger) :
IIntegrationEventHandler<UserTypologyDeletedIntegrationEvent>
{
    public async Task Handle(UserTypologyDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeleteUserTypologyCommand>(@event);

        await mediator.Send(command);
    }
}