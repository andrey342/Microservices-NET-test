namespace UserManagement.API.Application.Commands.PersonalResourceCommands.UpdatePersonalResource;

public class UpdatePersonalResourceCommand : IRequest<Result<Unit>>
{
    public UpdatePersonalResourceRequest Request { get; }

    public UpdatePersonalResourceCommand(UpdatePersonalResourceRequest request)
    {
        Request = request;
    }
}
