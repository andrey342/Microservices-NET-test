using UserManagement.API.Application.Commands.UserCommands.UpdateUser;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.UpdateServiceContract;

public class UpdateServiceContractRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UpdateUserRequest? UserRequest { get; set; }
    public Guid CurrentStatusId { get; set; }
    public Guid? ServiceContractStatusReasonId { get; set; }
    public Guid WorkCenterId { get; set; }
    public Guid ServiceTypeId { get; set; }
    public Guid UserTypeId { get; set; }
    public Guid UserTypologyId { get; set; }
}
