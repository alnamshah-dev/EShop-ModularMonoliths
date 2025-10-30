namespace Shared.Messaging.Events;

public class IntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredAt => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
