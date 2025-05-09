namespace UserManagement.API.Application.Commands.ResidenceCommands.DeleteResidence;

public class DeleteResidenceCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }


    public DeleteResidenceCommand(Guid id)
    {
        Id = id;
    }
}