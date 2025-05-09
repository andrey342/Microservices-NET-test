namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddBeneficiary;

public class AddBeneficiaryCommand : IRequest<Result<Guid>>
{
    public AddBeneficiaryRequest BeneficiaryRequest { get; private set; }

    public AddBeneficiaryCommand(AddBeneficiaryRequest beneficiaryRequest)
    {
        BeneficiaryRequest = beneficiaryRequest;
    }
}
