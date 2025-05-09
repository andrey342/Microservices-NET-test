using UserManagement.API.Application.Commands.UserTypeCommands.DeleteUserType;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class UserTypeDeletedIntegrationEventHandler(
    IMediator mediator,
    IMapper mapper,
    ILogger<UserTypeDeletedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<UserTypeDeletedIntegrationEvent>
{
    public async Task Handle(UserTypeDeletedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = mapper.Map<DeleteUserTypeCommand>(@event);

        await mediator.Send(command);
    }
}
