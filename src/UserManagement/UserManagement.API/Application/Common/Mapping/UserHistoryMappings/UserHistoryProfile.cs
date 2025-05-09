using UserManagement.API.Application.Queries.UserQueries;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.Events;

namespace UserManagement.API.Application.Common.Mapping.UserHistoryMappings;

public class UserHistoryProfile : Profile
{
    public UserHistoryProfile()
    {
        CreateMap<UserHistory, UserHistoryViewModel>();

        #region Create Mapping (Creación)

        CreateMap<UserHistoryDomainEvent, UserHistory>();
        #endregion
    }
}
