using Microsoft.IdentityModel.Tokens;
using Product.Domain.Enums;
using SharedKernel.Enums;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities.ProductAggregate.ConcreteProducts;

public sealed class LaptopProduct
    : BaseProduct
{
    public ProductOperatingSystem OperatingSystem { get; private set; }
    public Processor Processor { get; private set; }
    public Graphics Graphics { get; private set; }
    public Display Display { get; private set; }
    public Ram Ram { get; private set; }
    public StorageDevice Storage { get; private set; }
    public Battery Battery { get; private set; }

    private LaptopProduct() : base() { }

    private LaptopProduct(
        ProductDetails productDetails,
        BrandModel brandModel,
        DimensionsInfo dimensions,
        Color color,
        Material material,
        ProductOperatingSystem operatingSystem,
        Processor processor,
        Graphics graphics,
        Display display,
        Ram ram,
        StorageDevice storage,
        Battery battery)
        : base(
            ProductType.Laptop,
            productDetails,
            brandModel,
            dimensions,
            color,
            material)
    {
        OperatingSystem = operatingSystem;
        OperatingSystem = operatingSystem;
        Processor = processor;
        Graphics = graphics;
        Display = display;
        Ram = ram;
        Storage = storage;
        Battery = battery;
    }

    public static Result<LaptopProduct> Create(
        ProductDetails productDetails,
        BrandModel brandModel,
        DimensionsInfo dimensions,
        Color color,
        Material material,
        ProductOperatingSystem operatingSystem,
        Processor processor,
        Graphics graphics,
        Display display,
        Ram ram,
        StorageDevice storage,
        Battery battery)
    {
        return new LaptopProduct(
            productDetails,
            brandModel,
            dimensions,
            color,
            material,
            operatingSystem,
            processor,
            graphics,
            display,
            ram,
            storage,
            battery);
    }

    public new Result Edit(
        BrandModel? brandModel,
        ProductDetails? productDetails,
        DimensionsInfo? dimentions,
        Color? color,
        Material? material,
        ProductOperatingSystem? operatingSystem,
        Processor? processor,
        Graphics? graphics,
        Display? display,
        Ram? ram,
        StorageDevice? storage,
        Battery? battery)
    {
        var editResult = Edit(brandModel, productDetails, dimentions, color, material);

        if (editResult.IsFailure)
        {
            return editResult;
        }

        if (operatingSystem is not null && operatingSystem != OperatingSystem)
        {
            OperatingSystem = operatingSystem.Value;
        }

        if (processor is not null && processor != Processor)
        {
            Processor = processor;
        }

        if (graphics is not null && graphics != Graphics)
        {
            Graphics = graphics;
        }

        if (display is not null && display != Display)
        {
            Display = display;
        }

        if (ram is not null && ram != Ram)
        {
            Ram = ram;
        }

        if (storage is not null && storage != Storage)
        {
            Storage = storage;
        }

        if (battery is not null && battery != Battery)
        {
            Battery = battery;
        }


        return Result.Success();
    }
}
