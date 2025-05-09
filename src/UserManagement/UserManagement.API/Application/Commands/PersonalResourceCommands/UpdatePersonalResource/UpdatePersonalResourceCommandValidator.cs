using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.PersonalResourceCommands.UpdatePersonalResource;

public class UpdatePersonalResourceCommandValidator : BaseValidator<UpdatePersonalResourceCommand>
{
    public UpdatePersonalResourceCommandValidator(IServiceScopeFactory factory)
        : base(factory)
    {
        ValidateGuid<PersonalResource>(x => x.Request.Id, true);
        ValidateString(x => x.Request.Name, 100, true);
        ValidateString(x => x.Request.Observations, 200, false);
        ValidatePhoneNumber(x => x.Request.Phone, false);
        ValidatePhoneNumber(x => x.Request.Mobile, false);
        ValidateGuid<User>(x => x.Request.UserId, true);
    }
}
