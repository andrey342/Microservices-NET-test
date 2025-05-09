using UserManagement.API.Application.Common.Validation;

namespace UserManagement.API.Application.Commands.PersonalResourceCommands.DeletePersonalResource;

public class DeletePersonalResourceCommandValidator : BaseValidator<DeletePersonalResourceCommand>
{
    public DeletePersonalResourceCommandValidator(IServiceScopeFactory factory)
        : base(factory)
    {
        ValidateGuid<PersonalResource>(x => x.Id, true);
    }
}