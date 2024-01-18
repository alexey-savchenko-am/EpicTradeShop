using System.Text.Json.Serialization;

namespace Product.Domain.Enums;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductStatus
{
    Draft = 0,
    Active = 1,
    Suspended = 2,
}
