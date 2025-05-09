using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;

namespace UserManagement.API.Application.Commands.MedicationCommands.UpdateMedication;

public class UpdateMedicationCommandValidator : BaseValidator<UpdateMedicationCommand>
{
    public UpdateMedicationCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<Medication>(x => x.UpdateMedicationRequest.Id, isRequired: true);
        ValidateString(x => x.UpdateMedicationRequest.Dosage, maxLength: 100, isRequired: false);
    }
}