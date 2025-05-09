using UserManagement.API.Application.Commands.WorkCenterCommands.CreateWorkCenter;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class WorkCenterCreatedIntegrationEventHandler
(
    IMediator mediator,
    ILogger<WorkCenterCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<WorkCenterCreatedIntegrationEvent>
{
    public async Task Handle(WorkCenterCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = new CreateWorkCenterCommand(@event.Id, @event.Name);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.Name),
            command.Name,
            command);

        await mediator.Send(command);
    }
}
