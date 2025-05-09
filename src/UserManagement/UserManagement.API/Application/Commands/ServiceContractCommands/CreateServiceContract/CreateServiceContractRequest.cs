using UserManagement.API.Application.Commands.UserCommands.CreateUser;

namespace UserManagement.API.Application.Commands.ServiceContractCommands.CreateServiceContract
{
    public class CreateServiceContractRequest
    {
        public Guid? UserId { get; set; } // Puede ser null si el usuario no existe
        public CreateUserRequest? UserRequest { get; set; } // Información del usuario si necesita ser creado
        public Guid WorkCenterId { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid UserTypologyId { get; set; }
    }
}