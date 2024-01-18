using System.Text.Json.Serialization;

namespace SharedKernel.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Brand
{
    Apple,
    HUAWEI,
    MSI,
    Digma,
    Xiaomi,
    TECHNO,
    ASUS,
    Lenovo,
    CHUWI,
    HONOR,
    Samsung,
    INFINIX,
    AMD,
    Intel,
    Nvidia,
}
