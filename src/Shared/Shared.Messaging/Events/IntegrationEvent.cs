namespace Shared.Messaging.Events;

public record IntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredAt => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
