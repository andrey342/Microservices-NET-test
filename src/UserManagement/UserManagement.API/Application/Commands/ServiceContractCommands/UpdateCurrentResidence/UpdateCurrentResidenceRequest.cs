namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateCurrentResidence;

public class UpdateCurrentResidenceRequest
{
    public Guid Id { get; set; }
    public Guid ResidenceId { get; set; }
}