using System.Text.Json.Serialization;

namespace Product.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BatteryType
{
    LiIon,
    LiPol
}
