using Confluent.Kafka;

namespace EventBusKafka;

public class EventBusOptions
{
    public int RetryCount { get; set; } = 2;
    public string BootstrapServers { get; set; }
    public string SchemaRegistryUrl { get; set; }
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;
    public string GroupId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConsumerUsername { get; set; }
    public string ConsumerPassword { get; set; }
}
