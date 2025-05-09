namespace IntegrationEventLog;

public class EventLock
{
    public Guid EventId { get; set; }
    public DateTime LockedAt { get; set; }
}
