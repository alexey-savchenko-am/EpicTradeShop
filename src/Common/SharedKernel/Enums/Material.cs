using System.Text.Json.Serialization;

namespace SharedKernel.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Material
{
    Plastic,
    Metal,
    MetalPlastic,
    Aluminum,
    CarbonFiber,
    MagnesiumAlloy,
    Glass,
    Leather
}
