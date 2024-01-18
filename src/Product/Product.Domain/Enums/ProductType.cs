using System.Text.Json.Serialization;

namespace Product.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductType
{
    Laptop = 1,
    Smartphone = 2,
    Tablet = 3,
    Headphones = 4,
    PersonalComputer = 5
}
