namespace Product.Application.Requests.Common;

public record DisplayRequest(decimal diagonalInch, int widthPixel, int heightPixel, int refreshRateGc, int viewingAngleDeg);
