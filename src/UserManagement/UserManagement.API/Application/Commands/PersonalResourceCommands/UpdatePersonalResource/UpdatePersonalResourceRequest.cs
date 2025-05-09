using UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;

namespace UserManagement.API.Application.Commands.PersonalResourceCommands.UpdatePersonalResource;

public class UpdatePersonalResourceRequest : CreatePersonalResourceRequest
{
    public Guid Id { get; set; }
}