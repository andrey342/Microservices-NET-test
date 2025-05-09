using System.Linq.Expressions;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Application.Common.Validation;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    protected BaseValidator(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// Valida un GUID con opción de ser obligatorio u opcional, verificando su existencia en la BD.
    /// Se crea un nuevo scope de servicio para evitar problemas de concurrencia con DbContext.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad asociada al GUID.</typeparam>
    /// <param name="propertyExpression">Expresión de la propiedad GUID.</param>
    /// <param name="isRequired">Define si el GUID es obligatorio.</param>
    protected void ValidateGuid<TEntity>(
        Expression<Func<T, Guid?>> propertyExpression,
        bool isRequired = false) where TEntity : class
    {
        var propertyName = GetPropertyName(propertyExpression);

        // Si es requerido, aplica NotEmpty primero (para Guid.Empty)
        if (isRequired)
        {
            RuleFor(propertyExpression)
                .Must(id => id != Guid.Empty)
                .WithMessage($"{propertyName} is required.");
        }

        // Solo si el GUID tiene un valor distinto de `Guid.Empty`, verificar en la BD
        RuleFor(propertyExpression)
            .MustAsync(async (id, cancellation) =>
            {
                if (!id.HasValue || id == Guid.Empty) return !isRequired; // Si es null o vacío y NO requerido, pasa validación.

                using var scope = _serviceScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TEntity>>();
                return await repository.ExistsAsync(id.Value);
            })
            .When(x => propertyExpression.Compile().Invoke(x) != Guid.Empty) // Solo validar si tiene un valor real
            .WithMessage($"{propertyName} is invalid.");
    }


    /// <summary>
    /// Valida un string con opción de ser obligatorio u opcional, incluyendo longitud máxima.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad string.</param>
    /// <param name="maxLength">Longitud máxima permitida. Por defecto 100.</param>
    /// <param name="isRequired">Define si el campo es obligatorio.</param>
    protected void ValidateString(
        Expression<Func<T, string?>> propertyExpression,
        int maxLength = 100,
        bool isRequired = false)
    {
        var propertyName = GetPropertyName(propertyExpression);

        RuleFor(propertyExpression)
            .NotEmpty().WithMessage($"{propertyName} is required.")
            .When(_ => isRequired) //  Si es requerido, valida "is required"
            .DependentRules(() => //  Solo si no está vacío, valida la longitud
            {
                RuleFor(propertyExpression)
                    .MaximumLength(maxLength)
                    .WithMessage($"{propertyName} must not exceed {maxLength} characters.")
                    .When(x => !string.IsNullOrWhiteSpace(propertyExpression.Compile().Invoke(x)));
            });
    }

    /// <summary>
    /// Valida un booleano con opción de ser obligatorio.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad booleana.</param>
    /// <param name="isRequired">Define si el booleano es obligatorio.</param>
    protected void ValidateBoolean(
        Expression<Func<T, bool?>> propertyExpression,
        bool isRequired = false)
    {
        var propertyName = GetPropertyName(propertyExpression);

        RuleFor(propertyExpression)
            .NotNull().WithMessage($"{propertyName} is required.")
            .When(_ => isRequired);
    }

    /// <summary>
    /// Valida un decimal con opción de ser obligatorio u opcional, asegurando que tenga la precisión y escala especificadas.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad decimal.</param>
    /// <param name="isRequired">Define si el decimal es obligatorio.</param>
    /// <param name="precision">Número total de dígitos permitidos.</param>
    /// <param name="scale">Número de dígitos permitidos después del punto decimal.</param>
    protected void ValidateDecimal(
        Expression<Func<T, decimal?>> propertyExpression,
        bool isRequired = false,
        int precision = int.MaxValue,
        int scale = int.MaxValue)
    {
        var propertyName = GetPropertyName(propertyExpression);

        RuleFor(propertyExpression)
            .NotNull().WithMessage($"{propertyName} is required.")
            .When(_ => isRequired)
            .DependentRules(() =>
            {
                RuleFor(propertyExpression)
                    .Must(value =>
                    {
                        if (!value.HasValue) return true;
                        var parts = value.ToString().Split('.');
                        var integerDigits = parts[0].Length;
                        var fractionalDigits = parts.Length > 1 ? parts[1].Length : 0;
                        return integerDigits + fractionalDigits <= precision && fractionalDigits <= scale;
                    })
                    .WithMessage($"{propertyName} must have up to {precision - scale} digits before and {scale} digits after the decimal point.")
                    .When(x => propertyExpression.Compile().Invoke(x).HasValue);
            });
    }

    /// <summary>
    /// Valida una fecha con opción de ser obligatoria u opcional, asegurando que esté en el pasado o futuro.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad de fecha.</param>
    /// <param name="isRequired">Define si la fecha es obligatoria.</param>
    /// <param name="isFutureDate">Define si la fecha tiene que ser en el futuro.</param>
    protected void ValidateDate(
        Expression<Func<T, DateTime?>> propertyExpression,
        bool isRequired = false,
        bool isFutureDate = false)
    {
        var propertyName = GetPropertyName(propertyExpression);

        // Si es requerido, asegurarse de que no sea null ni la fecha por defecto de .NET (0001-01-01)
        if (isRequired)
        {
            RuleFor(propertyExpression)
                .Must(date => date.HasValue && date.Value != DateTime.MinValue)
                .WithMessage($"{propertyName} is required.");
        }

        // Si tiene un valor válido, verificar si debe estar en el pasado o futuro
        RuleFor(propertyExpression)
            .Must(date => !date.HasValue || (isFutureDate ? date >= DateTime.UtcNow : date <= DateTime.UtcNow))
            .WithMessage(isFutureDate
                ? $"{propertyName} must be in the future."
                : $"{propertyName} must be in the past.")
            .When(x => propertyExpression.Compile().Invoke(x).HasValue && propertyExpression.Compile().Invoke(x) != DateTime.MinValue);
    }

    /// <summary>
    /// Valida el formato de un email con opción de ser obligatorio u opcional.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad email.</param>
    /// <param name="isRequired">Define si el email es obligatorio.</param>
    protected void ValidateEmail(Expression<Func<T, string?>> propertyExpression, bool isRequired = false)
    {
        var propertyName = GetPropertyName(propertyExpression);

        RuleFor(propertyExpression)
            .NotEmpty().WithMessage($"{propertyName} is required.") //  Primero validar "is required"
            .When(_ => isRequired)
            .DependentRules(() => //  Si pasa la validación de required, valida el formato
            {
                RuleFor(propertyExpression)
                    .EmailAddress().WithMessage("Invalid email format.")
                    .When(x => !string.IsNullOrWhiteSpace(propertyExpression.Compile().Invoke(x)));
            });
    }

    /// <summary>
    /// Valida el formato de un número de teléfono en formato internacional con opción de ser obligatorio u opcional.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad teléfono.</param>
    /// <param name="isRequired">Define si el número de teléfono es obligatorio.</param>
    protected void ValidatePhoneNumber(Expression<Func<T, string?>> propertyExpression, bool isRequired = false)
    {
        var propertyName = GetPropertyName(propertyExpression);

        RuleFor(propertyExpression)
            .NotEmpty().WithMessage($"{propertyName} is required.") //  Primero validar "is required"
            .When(_ => isRequired)
            .DependentRules(() => //  Si pasa la validación de required, valida el formato
            {
                RuleFor(propertyExpression)
                    .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage($"{propertyName} format is invalid.")
                    .When(x => !string.IsNullOrWhiteSpace(propertyExpression.Compile().Invoke(x)));
            });
    }

    /// <summary>
    /// Valida que un número sea positivo.
    /// </summary>
    /// <param name="propertyExpression">Expresión de la propiedad numérica.</param>
    /// <param name="isRequired">Define si el número es obligatorio.</param>
    protected void ValidatePositiveNumber(
        Expression<Func<T, int?>> propertyExpression,
        bool isRequired = false)
    {
        var propertyName = GetPropertyName(propertyExpression);

        // Si es requerido, aplica NotEmpty primero
        if (isRequired)
        {
            RuleFor(propertyExpression)
                .NotEmpty()
                .WithMessage($"{propertyName} is required.");
        }

        // Validar que el número sea positivo
        RuleFor(propertyExpression)
            .Must(number => !number.HasValue || number > 0)
            .WithMessage($"{propertyName} must be a positive number.")
            .When(x => propertyExpression.Compile().Invoke(x).HasValue);
    }

    /// <summary>
    /// Obtiene el nombre de una propiedad a partir de su expresión lambda.
    /// </summary>
    /// <typeparam name="TProperty">Tipo de la propiedad.</typeparam>
    /// <param name="expression">Expresión lambda de la propiedad.</param>
    /// <returns>Nombre de la propiedad.</returns>
    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        else if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
        {
            return operand.Member.Name;
        }

        throw new ArgumentException($"Invalid property expression: {expression}");
    }
}
