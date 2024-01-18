namespace Product.Application.Requests.Common;

public record DisplayRequest(
    decimal DiagonalInch, 
    int WidthPixel, 
    int HeightPixel, 
    int RefreshRateGc, 
    int ViewingAngleDeg);
