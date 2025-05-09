using MediatR;

namespace UserManagement.Domain.SeedWork;
public interface IDomainEvent: INotification
{
    DateTime OccurredOn { get; }
}
