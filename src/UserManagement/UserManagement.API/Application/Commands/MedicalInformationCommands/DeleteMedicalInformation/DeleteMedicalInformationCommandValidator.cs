using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.MedicalInformationCommands.DeleteMedicalInformation;

public class DeleteMedicalInformationCommandValidator : BaseValidator<DeleteMedicalInformationCommand>
{
    public DeleteMedicalInformationCommandValidator(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<User>(x => x.Id, isRequired: true);
    }
}