using System.Diagnostics;
using OpenTelemetry.Context.Propagation;

namespace EventBusKafka;

public class KafkaTelemetry
{
    public static string ActivitySourceName = "EventBusKafka";

    public ActivitySource ActivitySource { get; } = new(ActivitySourceName);
    public TextMapPropagator Propagator { get; } = Propagators.DefaultTextMapPropagator;
}
