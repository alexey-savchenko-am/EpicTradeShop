using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities;

public class ScreenResolution
    : ValueObject
{
    public int Width { get; }
    public int Height { get; }

    private ScreenResolution() { }

    private ScreenResolution(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static Result<ScreenResolution> Create(int width, int height)
    {
        if(width <= 0)
        {
            return new Error("ScreenResolution.Create", "Screen width can not be less or equals zero");
        }
        if (height <= 0)
        {
            return new Error("ScreenResolution.Create", "Screen height can not be less or equals zero");
        }

        return new ScreenResolution(width, height);
    }

    public override string ToString()
        => $"{Width}X{Height}";

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Width;
        yield return Height;
    }
}
