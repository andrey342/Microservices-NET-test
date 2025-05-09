using UserManagement.API.Application.Commands.UserCommands.CreateUser;
using UserManagement.API.Application.Commands.UserCommands.DeleteUser;
using UserManagement.API.Application.Commands.UserCommands.UpdateUser;
using UserManagement.Infrastructure.Idempotency;

namespace UserManagement.API.Application.Commands.IdentifiedCommands;

/// <summary>
/// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
/// a requestid sent by client is used to detect duplicate requests.
/// </summary>
/// <typeparam name="TCommand">Type of the command handler that performs the operation if request is not duplicated</typeparam>
/// <typeparam name="TResult">Return value of the inner command handler</typeparam>
public class IdentifiedCommandHandler<TCommand, TResult> : IRequestHandler<IdentifiedCommand<TCommand, TResult>, TResult>
    where TCommand : IRequest<TResult>
{
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;
    private readonly ILogger<IdentifiedCommandHandler<TCommand, TResult>> _logger;

    public IdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<TCommand, TResult>> logger)
    {
        _mediator = mediator;
        _requestManager = requestManager;
        _logger = logger;
    }

    /// <summary>
    /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
    /// just enqueues the original inner command.
    /// </summary>
    /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
    /// <returns>Return value of inner command or default value if request same ID was found</returns>
    public async Task<TResult> Handle(IdentifiedCommand<TCommand, TResult> request, CancellationToken cancellationToken)
    {
        var alreadyExists = await _requestManager.ExistAsync(request.Id);
        if (alreadyExists)
        {
            throw new Exception($"Duplicate request");
        }
        else
        {
            await _requestManager.CreateRequestForCommandAsync<TCommand>(request.Id);
            try
            {
                var command = request.Command;
                var commandName = command.GetGenericTypeName();
                var idProperty = string.Empty;
                var commandId = string.Empty;

                // Esto ayuda a: Identificar qué operación se está ejecutando (Crear, Actualizar, Eliminar).
                // Saber qué usuario está involucrado en la operación.
                // Facilitar la depuración en caso de errores.
                switch (command)
                {
                    case CreateUserCommand createUserCommand:
                        idProperty = nameof(createUserCommand.UserRequest.Name);
                        commandId = $"{createUserCommand.UserRequest.Name}";
                        break;

                    case UpdateUserCommand updateUserCommand:
                        idProperty = nameof(updateUserCommand.UserRequest.Id);
                        commandId = $"{updateUserCommand.UserRequest.Id}";
                        break;

                    case DeleteUserCommand deleteUserCommand:
                        idProperty = nameof(deleteUserCommand.Id);
                        commandId = $"{deleteUserCommand.Id}";
                        break;

                    default:
                        idProperty = "Id?";
                        commandId = "n/a";
                        break;
                }

                _logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    commandName,
                    idProperty,
                    commandId,
                    command);

                // Send the embedded business command to mediator so it runs its related CommandHandler 
                var result = await _mediator.Send(command, cancellationToken);

                _logger.LogInformation(
                    "Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    result,
                    commandName,
                    idProperty,
                    commandId,
                    command);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
