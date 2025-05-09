namespace UserManagement.API.Application.Commands.CentralUnitCommands.CreateCentralUnit;

public class CreateCentralUnitCommand : IRequest<Result<Guid>>
{
    public CreateCentralUnitRequest CentralUnitRequest { get; private set; }

    public CreateCentralUnitCommand(CreateCentralUnitRequest centralUnitRequest)
    {
        CentralUnitRequest = centralUnitRequest;
    }
}