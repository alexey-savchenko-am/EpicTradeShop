using SharedKernel.Output;

namespace SharedKernel.ValueObjects;


public class DimensionsInfo : ValueObject
{
    /// <summary>
    /// Length in cm
    /// </summary>
    public decimal Length { get; }

    /// <summary>
    /// Width in cm
    /// </summary>
    public decimal Width { get; }

    /// <summary>
    /// Height in cm
    /// </summary>
    public decimal Height { get; }

    /// <summary>
    /// Weight in kg
    /// </summary>
    public decimal Weight { get; }

    public decimal Volume => Length * Width * Height;

    private DimensionsInfo() { }

    private DimensionsInfo(decimal length, decimal width, decimal height, decimal weight)
    {
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
    }

    public static Result<DimensionsInfo> Create(decimal length, decimal width, decimal height, decimal weight)
    {
        if (length <= 0 || width <= 0 || height <= 0)
        {
            return new Error("DimensionsInfo.Create", "Size can not be less or equals zero.");
        }

        if(weight <= 0)
        {
            return new Error("DimensionsInfo.Create", "Weight can not be less or equals zero.");
        }

        return new DimensionsInfo(length, width, height, weight);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Length;
        yield return Width;
        yield return Height;
        yield return Weight;
    }
}

