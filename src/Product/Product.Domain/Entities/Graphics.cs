using Product.Domain.Enums;
using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities;

public class Graphics
    : ValueObject
{
    public GraphicsControllerType GraphicsControllerType { get; }
    public BrandModel BrandModel { get; }
    public int VideoMemoryVolumeMb { get; }

    private Graphics() { }

    public Graphics(GraphicsControllerType graphicsControllerType, BrandModel brandModel, int videoMemoryVolumeMb )
	{
        GraphicsControllerType = graphicsControllerType;
        BrandModel = brandModel;
        VideoMemoryVolumeMb = videoMemoryVolumeMb;
    }

    public static Result<Graphics> Create(GraphicsControllerType graphicsControllerType, BrandModel brandModel, int videoMemoryVolumeMb)
	{
        return new Graphics(graphicsControllerType, brandModel, videoMemoryVolumeMb);
	}

    public static Result<Graphics> CreateIntegrated(BrandModel brandModel, int videoMemoryVolumeMb)
        => Create(GraphicsControllerType.Integrated, brandModel, videoMemoryVolumeMb);
    

    public static Result<Graphics> CreateDiscrete(BrandModel brandModel, int videoMemoryVolumeMb)
        => Create(GraphicsControllerType.Discrete, brandModel, videoMemoryVolumeMb);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return GraphicsControllerType;
        yield return BrandModel;
        yield return VideoMemoryVolumeMb;
    }
}
