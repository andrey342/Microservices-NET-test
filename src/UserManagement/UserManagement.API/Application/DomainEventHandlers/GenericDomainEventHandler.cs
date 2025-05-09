using UserManagement.Domain.SeedWork;

namespace UserManagement.API.Application.DomainEventHandlers;

public class GenericDomainEventHandler : INotificationHandler<IDomainEvent>
{
    public Task Handle(IDomainEvent notification, CancellationToken cancellationToken)
    {
        // Registro básico de logs
        Console.WriteLine($"Domain Event Occurred: {notification.GetType().Name} at {notification.OccurredOn}");

        // Aquí puedes incluir lógica genérica, como:
        // - Guardar logs en un sistema externo
        // - Enviar métricas a Prometheus o un sistema de monitoreo
        // - Ejecutar validaciones globales
        // - Enviar eventos por el kafka

        return Task.CompletedTask;
    }
}
