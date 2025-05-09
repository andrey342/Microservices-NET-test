using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.MedicationCommands.DeleteMedication;

public class DeleteMedicationCommandValidator : BaseValidator<DeleteMedicationCommand>
{
    public DeleteMedicationCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Medication>(x => x.Id, isRequired: true);
    }
}