using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace UserManagement.Domain.SeedWork;
public abstract class Entity
{
    public Guid Id { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity()
    {
        // BUG FIX: Si inicializamos el Id en el contructor EF va a creer que ese Entity ya existe en la base de datos
        // SOLUCION: Comentar la inicializacion y definir en cada DbContext que el id se crea en base de datos
        //Id = Guid.NewGuid();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (Entity)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    // -------------------------------
    // Método RÁPIDO para < 10 propiedades
    // -------------------------------
    /// <summary>
    /// Copia automáticamente las propiedades de otro objeto del mismo tipo (para pocos atributos).
    /// Mantiene la lógica de Value Objects y evita copiar colecciones.
    /// </summary>
    public void CopyPropertiesTo<T>(T source) where T : Entity
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => prop.CanRead && prop.CanWrite &&
                !typeof(IEnumerable<object>).IsAssignableFrom(prop.PropertyType) && // Excluye IEnumerable<T>
                (prop.PropertyType.IsPrimitive ||
                 prop.PropertyType.IsValueType ||
                 prop.PropertyType == typeof(string) ||
                 typeof(ValueObject).IsAssignableFrom(prop.PropertyType))); // Excluye List<T>, HashSet<T>, etc.

        foreach (var prop in properties)
        {
            var sourceValue = prop.GetValue(source);
            // Si es un Value Object, se instancia nuevamente con su constructor
            if (typeof(ValueObject).IsAssignableFrom(prop.PropertyType))
            {
                if (sourceValue != null)
                {
                    var constructor = prop.PropertyType.GetConstructor(new[] { sourceValue.GetType() });
                    if (constructor != null)
                    {
                        var newEntityInstance = constructor.Invoke(new[] { sourceValue });
                        prop.SetValue(this, newEntityInstance);
                    }
                }
            }
            // Si es un Entity, se usa su constructor si existe
            else if (typeof(Entity).IsAssignableFrom(prop.PropertyType))
            {
                if (sourceValue != null)
                {
                    var constructor = prop.PropertyType.GetConstructor(new[] { sourceValue.GetType() });
                    if (constructor != null)
                    {
                        var newEntityInstance = constructor.Invoke(new[] { sourceValue });
                        prop.SetValue(this, newEntityInstance);
                    }
                }
            }
            // Si es un tipo primitivo, se copia directamente
            else
            {
                prop.SetValue(this, sourceValue);
            }
        }
    }

    // -------------------------------
    // Método MUY RÁPIDO para +10 propiedades
    // -------------------------------
    /// <summary>
    /// Copia automáticamente las propiedades de otro objeto del mismo tipo de forma optimizada.
    /// Usa expresiones compiladas y paralelización para mejorar rendimiento en objetos grandes.
    /// </summary>
    public void CopyPropertiesFast<T>(T source) where T : Entity
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => prop.CanRead && prop.CanWrite &&
                !typeof(IEnumerable<object>).IsAssignableFrom(prop.PropertyType) && // Excluye IEnumerable<T>
                (prop.PropertyType.IsPrimitive ||
                 prop.PropertyType.IsValueType ||
                 prop.PropertyType == typeof(string) ||
                 typeof(ValueObject).IsAssignableFrom(prop.PropertyType)))// Excluye List<T>, HashSet<T>, etc.
            .ToArray(); // Convierte a array para optimización en paralelo

        // Crear cache de setters dinámicos para evitar Reflection en cada iteración
        var setters = new ConcurrentDictionary<PropertyInfo, Action<T, object>>();

        Parallel.ForEach(properties, prop =>
        {
            var setter = setters.GetOrAdd(prop, CreateSetter<T>(prop)); // Obtiene o crea un setter compilado
            var sourceValue = prop.GetValue(source);
            // Si es un Value Object, instanciar uno nuevo
            if (typeof(ValueObject).IsAssignableFrom(prop.PropertyType))
            {
                if (sourceValue != null)
                {
                    var constructor = prop.PropertyType.GetConstructor(new[] { sourceValue.GetType() });
                    if (constructor != null)
                    {
                        var newValueObject = constructor.Invoke(new[] { sourceValue });
                        setter(this as T, newValueObject);
                    }
                }
            }
            // Si es un Entity, se usa su constructor si existe
            else if (typeof(Entity).IsAssignableFrom(prop.PropertyType))
            {
                if (sourceValue != null)
                {
                    var constructor = prop.PropertyType.GetConstructor(new[] { sourceValue.GetType() });
                    if (constructor != null)
                    {
                        var newEntityInstance = constructor.Invoke(new[] { sourceValue });
                        setter(this as T, newEntityInstance);
                    }
                }
            }
            // Si es primitivo, copiar directamente
            else
            {
                setter(this as T, sourceValue);
            }
        });
    }

    /// <summary>
    /// Genera una expresión lambda para asignar valores a una propiedad de forma eficiente.
    /// </summary>
    private static Action<T, object> CreateSetter<T>(PropertyInfo prop) where T : Entity
    {
        var targetExp = Expression.Parameter(typeof(T), "target");
        var valueExp = Expression.Parameter(typeof(object), "value");

        var convertedValueExp = Expression.Convert(valueExp, prop.PropertyType);
        var propertyExp = Expression.Property(targetExp, prop);
        var assignExp = Expression.Assign(propertyExp, convertedValueExp);

        return Expression.Lambda<Action<T, object>>(assignExp, targetExp, valueExp).Compile();
    }
}
