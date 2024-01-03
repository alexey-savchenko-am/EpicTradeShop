using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities;

public class Display
    : ValueObject
{
    public decimal ScreenDiagonalInch { get; }
    public ScreenResolution ScreenResolution { get; }
    public int RefreshRateGc { get; }
    public int ViewingAngleDeg { get; }

    private Display() { }

    private Display(
        decimal screenDiagonalInch,
        ScreenResolution screenResolution,
        int refreshRateGc,
        int viewingAngleDeg)
    {
        ScreenDiagonalInch = screenDiagonalInch;
        ScreenResolution = screenResolution;
        RefreshRateGc = refreshRateGc;
        ViewingAngleDeg = viewingAngleDeg;
    }

    public static Result<Display> Create(
        decimal screenDiagonalInch,
        ScreenResolution screenResolution,
        int refreshRateGc,
        int viewingAngleDeg)
    {
        return new Display(screenDiagonalInch, screenResolution, refreshRateGc, viewingAngleDeg);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ScreenDiagonalInch;
        yield return ScreenResolution;
        yield return RefreshRateGc;
        yield return ViewingAngleDeg;
    }
}
