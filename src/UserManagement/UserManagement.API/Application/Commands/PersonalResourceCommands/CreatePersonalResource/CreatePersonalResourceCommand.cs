namespace UserManagement.API.Application.Commands.PersonalResourceCommands.CreatePersonalResource;

public class CreatePersonalResourceCommand : IRequest<Result<Guid>>
{
    public CreatePersonalResourceRequest Request { get; }

    public CreatePersonalResourceCommand(CreatePersonalResourceRequest request)
    {
        Request = request;
    }
}