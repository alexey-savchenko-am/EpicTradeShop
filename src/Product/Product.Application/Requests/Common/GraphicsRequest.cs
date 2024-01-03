namespace Product.Application.Requests.Common;

public record GraphicsRequest(
    bool IsDiscrete,
    string VideoControllerBrand,
    string VideoControllerModel,
    int VolumeMb);
