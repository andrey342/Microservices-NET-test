using UserManagement.API.Application.Common.Models;

namespace UserManagement.API.Application.Commands.CohabitantCommands.DeleteCohabitant;

public class DeleteCohabitantCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public DeleteCohabitantCommand(Guid id)
    {
        Id = id;
    }
}