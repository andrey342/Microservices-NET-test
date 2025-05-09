using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Events;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.DomainEventHandlers;

public class UserHistoryDomainEventHandler : INotificationHandler<UserHistoryDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserHistoryDomainEventHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(UserHistoryDomainEvent notification, CancellationToken cancellationToken)
    {
        var userHistory = _mapper.Map<UserHistory>(notification);
        _userRepository.AddUserHistory(userHistory);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
