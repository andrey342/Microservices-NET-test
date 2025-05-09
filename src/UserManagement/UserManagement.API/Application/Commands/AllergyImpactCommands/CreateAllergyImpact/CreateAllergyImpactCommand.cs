using UserManagement.API.Application.Commands.MedicalConditionCommands.AddMedicalCondition;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.CreateAllergyImpact;

public class CreateAllergyImpactCommand : IRequest<Result<Guid>>
{
    public CreateAllergyImpactRequest CreateAllergyImpactRequest { get; private set; }

    public CreateAllergyImpactCommand(CreateAllergyImpactRequest createAllergyImpactRequest)
    {
        CreateAllergyImpactRequest = createAllergyImpactRequest;
    }
}