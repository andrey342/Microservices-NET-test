namespace EventBus.Events;

public record IntegrationEvent
{
    public IntegrationEvent()
    {
        IntegrationEventId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [JsonInclude]
    public Guid IntegrationEventId { get; set; }

    [JsonInclude]
    public DateTime CreationDate { get; set; }
}
