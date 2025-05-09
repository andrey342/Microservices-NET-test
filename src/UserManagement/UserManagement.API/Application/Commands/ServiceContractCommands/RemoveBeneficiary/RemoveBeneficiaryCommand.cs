namespace UserManagement.API.Application.Commands.ServiceContractCommands.RemoveBeneficiary;

public class RemoveBeneficiaryCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public RemoveBeneficiaryCommand(Guid id)
    {
        Id = id;
    }
}