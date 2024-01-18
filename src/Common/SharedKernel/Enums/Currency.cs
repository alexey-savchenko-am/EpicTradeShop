using System.Text.Json.Serialization;

namespace SharedKernel.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
    USD,
    EUR,
    GPB,
    JPY,
    RUB
}
