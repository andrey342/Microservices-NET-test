namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddCentralUnit;

public class AddCentralUnitCommand : IRequest<Result<Guid>>
{
    public AddCentralUnitRequest CentralUnitRequest { get; private set; }

    public AddCentralUnitCommand(AddCentralUnitRequest centralUnitRequest)
    {
        CentralUnitRequest = centralUnitRequest;
    }
}