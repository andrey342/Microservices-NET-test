using UserManagement.API.Application.Commands.IdentifiedCommands;

public class IdentifiedCommandValidator<TCommand, TResult> : AbstractValidator<IdentifiedCommand<TCommand, TResult>>
    where TCommand : IRequest<TResult>
{
    public IdentifiedCommandValidator(ILogger<IdentifiedCommandValidator<TCommand, TResult>> logger)
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Request ID is required.");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}