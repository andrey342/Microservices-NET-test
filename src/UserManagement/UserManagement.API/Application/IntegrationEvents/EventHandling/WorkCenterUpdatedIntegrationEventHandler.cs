using UserManagement.API.Application.Commands.WorkCenterCommands.UpdateWorkCenter;
using UserManagement.API.Application.IntegrationEvents.Events;

namespace UserManagement.API.Application.IntegrationEvents.EventHandling;

public class WorkCenterUpdatedIntegrationEventHandler
(
    IMediator mediator,
    ILogger<WorkCenterUpdatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<WorkCenterUpdatedIntegrationEvent>
{
    public async Task Handle(WorkCenterUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.IntegrationEventId, @event);

        var command = new UpdateWorkCenterCommand(@event.Id, @event.Name);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.Id),
            command.Id,
            command);

        await mediator.Send(command);
    }
}
