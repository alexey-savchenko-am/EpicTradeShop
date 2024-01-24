using System.Text.Json.Serialization;

namespace Persistence;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageType
{
    DomainEvent,
    IntegrationEvent
}