using System.Text.Json.Serialization;

namespace Product.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductOperatingSystem
{
    None,
    Windows,
    MacOS,
    Linux,
    Android,
    iOS,
    ChromeOS,
    Unix,
    FreeBSD,
    Solaris,
    BlackBerryOS,
    WindowsMobile,
    Other
}