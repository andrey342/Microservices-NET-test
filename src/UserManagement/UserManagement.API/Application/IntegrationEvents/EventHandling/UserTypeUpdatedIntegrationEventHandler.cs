using UserManagement.API.Application.Commands.UserTypeCommands.UpdateUserType;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class UserTypeUpdatedIntegrationEventHandler(
IMediator mediator,
IMapper mapper,
ILogger<UserTypeCreatedIntegrationEventHandler> logger) :
IIntegrationEventHandler<UserTypeUpdatedIntegrationEvent>
{
    public async Task Handle(UserTypeUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<UpdateUserTypeCommand>(@event);

        await mediator.Send(command);
    }
}