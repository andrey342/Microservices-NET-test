using UserManagement.API.Application.Common.Validation;
using UserManagement.Domain.AggregatesModel.UserAggregate;
using UserManagement.Domain.AggregatesModel.UserAggregate.Masters;

namespace UserManagement.API.Application.Commands.MedicationCommands.CreateMedication;

public class CreateMedicationCommandValidator : BaseValidator<CreateMedicationCommand>
{
    public CreateMedicationCommandValidator(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        ValidateGuid<MedicalInformation>(x => x.CreateMedicationRequest.MedicalInformationId, isRequired: true);
        ValidateGuid<Medicine>(x => x.CreateMedicationRequest.MedicineId, isRequired: true);
        ValidateString(x => x.CreateMedicationRequest.Dosage, maxLength: 100, isRequired: false);
    }
}