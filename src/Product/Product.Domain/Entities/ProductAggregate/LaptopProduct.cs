using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities.ProductAggregate;

public sealed class LaptopProduct
    : BaseProduct
{
	public Processor Processor { get; }
	public Graphics Graphics { get; }
	public Display Display { get; }
	public Ram Ram { get; }
	public StorageDevice Storage { get; }

	private LaptopProduct():base() { }

	private LaptopProduct(
        ProductDetails productDetails,
        BrandModel brandModel, 
		DimensionsInfo dimensions,
        Processor processor,
		Graphics graphics,
        Display display,
        Ram ram,
        StorageDevice storage)
		: base(ProductType.Laptop, productDetails, brandModel, dimensions)
	{
        Processor = processor;
        Graphics = graphics;
        Display = display;
        Ram = ram;
        Storage = storage;
    }

	public static Result<LaptopProduct> Create(
		ProductDetails productDetails, 
		BrandModel brandModel, 
		DimensionsInfo dimensions,
        Processor processor,
		Graphics graphics,
        Display display,
        Ram ram,
        StorageDevice storage)
	{
		return new LaptopProduct(
			productDetails, 
			brandModel, 
			dimensions, 
			processor, 
			graphics,
			display, 
			ram, 
			storage);
	}

	public new Result Edit(
		BrandModel? brandModel, 
		ProductDetails? productDetails, 
		DimensionsInfo? dimentions)
	{
		var editResult = base.Edit(brandModel, productDetails, dimentions);
		if (editResult.IsFailure)
		{
			return editResult;
		}


		return Result.Success();
	}
}
