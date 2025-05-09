namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContractStatus;

public class UpdateServiceContractStatusRequest
{
    public Guid Id { get; set; }
    public Guid ServiceContractStatusId { get; set; }

    public Guid ServiceContractStatusReasonId { get; set; }
}
