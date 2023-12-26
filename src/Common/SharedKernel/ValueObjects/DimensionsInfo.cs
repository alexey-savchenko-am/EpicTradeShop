using SharedKernel.Output;

namespace SharedKernel.ValueObjects;


public class DimensionsInfo : ValueObject
{
    /// <summary>
    /// Length in cm
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Width in cm
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Height in cm
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Weight in grams
    /// </summary>
    public int Weight { get; }

    public decimal Volume => Length * Width * Height;

    public DimensionsInfo(int length, int width, int height, int weight)
    {
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
    }

    public static Result<DimensionsInfo> Create(int length, int width, int height, int weight)
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

