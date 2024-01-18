using System.Text.Json.Serialization;

namespace Product.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RamType
{
    DDR2,
    DDR3, 
    DDR4, 
    DDR5
}
