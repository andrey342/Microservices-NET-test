using UserManagement.API.Application.Commands.UserCommands.CreateUser;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.AddBeneficiary;

public class AddBeneficiaryRequest
{
    public Guid ServiceContractId { get; set; }
    public Guid? UserId { get; set; }
    public CreateUserRequest? User { get; set; } // Información del usuario si necesita ser creado
}
