namespace IntegrationEventLog.Services;

public class IntegrationEventLogService<TContext> : IIntegrationEventLogService, IDisposable
    where TContext : DbContext
{
    private volatile bool _disposedValue;
    private readonly TContext _context;
    private readonly Type[] _eventTypes;

    public IntegrationEventLogService(TContext context)
    {
        _context = context;
        _eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
            .GetTypes()
            .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
            .ToArray();
    }

    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var result = await _context.Set<IntegrationEventLogEntry>()
            .Where(e => e.TransactionId == transactionId && e.State == EventStateEnum.NotPublished)
            .ToListAsync();

        if (result.Count != 0)
        {
            return result.OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(_eventTypes.FirstOrDefault(t => t.Name == e.EventTypeShortName)));
        }

        return [];
    }

    public async Task<IntegrationEventLogEntry> RetrieveEventLogPendingToPublishByEventIdAsync(Guid EventId)
    {
        var result = await _context.Set<IntegrationEventLogEntry>()
            .Where(e => e.EventId == EventId && e.State == EventStateEnum.NotPublished)
            .FirstOrDefaultAsync();

        return result;
    }

    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

        _context.Database.UseTransaction(transaction.GetDbTransaction());
        _context.Set<IntegrationEventLogEntry>().Add(eventLogEntry);

        return _context.SaveChangesAsync();
    }

    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventStateEnum.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventStateEnum.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
    }

    private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
    {
        var eventLogEntry = _context.Set<IntegrationEventLogEntry>().Single(ie => ie.EventId == eventId);
        eventLogEntry.State = status;

        if (status == EventStateEnum.InProgress)
            eventLogEntry.TimesSent++;

        return _context.SaveChangesAsync();
    }

    //Añadir reintento envio kafka 
    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveFailedEventLogsAsync()
    {
        //imprime _context.Database.GetDbConnection().ConnectionString; // Línea para inspeccionar en el depurador
        Console.WriteLine(_context.Database.GetDbConnection().ConnectionString);

        var result = await _context.Set<IntegrationEventLogEntry>()
            .Where(e => e.State == EventStateEnum.PublishedFailed)
            .ToListAsync();

        if (result.Count != 0)
        {
            return result.OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(_eventTypes.FirstOrDefault(t => t.Name == e.EventTypeShortName)));
        }

        return Enumerable.Empty<IntegrationEventLogEntry>();
    }

    public async Task UnlockEventAsync(Guid eventId)
    {
        try
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM EventLock WHERE EventId = {0}", eventId);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error al desbloquear el evento con ID {eventId}", ex);
        }
    }

    public async Task<bool> TryLockEventAsync(Guid eventId)
    {
        try
        {
            _context.Database.ExecuteSqlRaw("INSERT INTO EventLock (EventId, LockedAt) VALUES ({0}, {1})", eventId, DateTime.UtcNow);
            return true;
        }
        catch (DbUpdateException)
        {
            // Si ocurre una excepción, significa que el evento ya está bloqueado
            return false;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _context.Dispose();
            }


            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
