using UserManagement.API.Application.Queries.UserQueries;

namespace UserManagement.API.Application.Commands.AllergyImpactCommands.UpdateAllergyImpact;

public class UpdateAllergyImpactCommand : IRequest<Result<AllergyImpactViewModel>>
{
    public UpdateAllergyImpactRequest UpdateAllergyImpactRequest { get; private set; }

    public UpdateAllergyImpactCommand(UpdateAllergyImpactRequest updateAllergyImpactRequest)
    {
        UpdateAllergyImpactRequest = updateAllergyImpactRequest;
    }
}