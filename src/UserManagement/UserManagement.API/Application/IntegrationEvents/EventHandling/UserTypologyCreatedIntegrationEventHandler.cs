using UserManagement.API.Application.Commands.UserTypologyCommands.CreateUserTypology;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class UserTypologyCreatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<UserTypologyCreatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<UserTypologyCreatedIntegrationEvent>
{
    public async Task Handle(UserTypologyCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<CreateUserTypologyCommand>(@event);

        await mediator.Send(command);
    }
}