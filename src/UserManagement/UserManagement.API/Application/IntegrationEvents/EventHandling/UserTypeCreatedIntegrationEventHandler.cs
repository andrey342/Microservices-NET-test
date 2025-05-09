using UserManagement.API.Application.Commands.UserTypeCommands.CreateUserType;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class UserTypeCreatedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<UserTypeCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<UserTypeCreatedIntegrationEvent>
{
    public async Task Handle(UserTypeCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<CreateUserTypeCommand>(@event);

        await mediator.Send(command);
    }
}
