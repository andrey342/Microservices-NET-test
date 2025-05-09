namespace UserManagement.API.Application.Commands.AllergyImpactCommands.DeleteAllergyImpact;

public class DeleteAllergyImpactCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public DeleteAllergyImpactCommand(Guid id)
    {
        Id = id;
    }
}
