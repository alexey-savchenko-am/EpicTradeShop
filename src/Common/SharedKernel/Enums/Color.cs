using System.Text.Json.Serialization;

namespace SharedKernel.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Color
{
    White,
    Black,
    Gray,
    Silver,
    Gold,
    Pink,
    Blue,
    Green,
    Red,
    Purple,
    Brown,
    Orange,
    Yellow,
    Cyan,
    Magenta,
    Beige,
    Turquoise,
    Olive,
    SpaceGray,
    RoseGold,
    MidnightBlack,
    CosmicGray,
    MysticBronze,
    CloudBlue,
    PhantomGreen,
    OrchidGray,
    ArcticWhite,
    AmberSunrise,
    EmeraldGreen,
    CoralBlue,
    AlpineGreen,
    AuraGlow,
    OceanBlue,
    PlatinumGold,
    BurgundyRed,
    PrismBlack,
    LilacPurple
}
