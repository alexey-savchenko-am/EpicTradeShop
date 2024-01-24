using Product.Domain.DomainEvents;
using Product.Domain.Enums;
using SharedKernel;
using SharedKernel.Enums;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities.ProductAggregate;

public class BaseProduct
    : AggregateRoot
{
	private List<Category> _categories = new();
	private List<ProductImage> _images = new();

    public ProductType Type { get; }
    public ProductDetails ProductDetails { get; private set; }
    public BrandModel BrandModel { get; private set; }
    public Money? Price { get; private set; }
	public int StockQuantity { get; private set; }
    public DimensionsInfo Dimensions { get; private set; }
    public Color Color { get; private set; }
    public Material Material { get; private set; }
    public ProductStatus Status { get; private set; }
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
	public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    #pragma warning disable CS8618
    protected BaseProduct() : base(new ID(Guid.NewGuid())){}

	protected BaseProduct(
        ProductType type, 
        ProductDetails productDetails, 
        BrandModel brandModel, 
        DimensionsInfo dimensions,
        Color color,
        Material material)
		: base(new ID(Guid.NewGuid()))
	{
        Type = type;
        ProductDetails = productDetails;
        BrandModel = brandModel;
        Dimensions = dimensions;
        Color = color;
        Material = material;
        StockQuantity = 0;
		Status = ProductStatus.Draft;
	}

	public Result AddCategories(IEnumerable<string> categories)
	{
        foreach(var category in categories)
        {
            _categories.Add(Category.Create(category));
        }
        return Result.Success();
    }

    public Result Edit(
        BrandModel? brandModel, 
        ProductDetails? productDetails, 
        DimensionsInfo? dimentions,
        Color? color,
        Material? material)
    {
        if (Status > ProductStatus.Draft) 
        {
            return ProductErrors.CanNotEditNonDraftProduct;
        }

        if (productDetails is not null && productDetails != ProductDetails)
        {
            ProductDetails = productDetails;
        }

        if (brandModel is not null && brandModel != BrandModel)
        {
            BrandModel = brandModel;
        }

        if (dimentions is not null && dimentions != Dimensions)
        {
            Dimensions = dimentions;
        }

        if (color is not null && color != Color)
        {
            Color = color.Value;
        }

        if (material is not null && material != Material)
        {
            Material = material.Value;
        }

        return Result.Success();
    }

    public Result ChangeStatus(ProductStatus newStatus)
    {
        if(Status == ProductStatus.Draft && newStatus == ProductStatus.Suspended)
        {
            return ProductErrors.AttemptToSuspendDraftProduct;
        }

        Status = newStatus;
        RaiseDomainEvent(new ProductStatusChangedDomainEvent(Id, newStatus));
        return Result.Success();
    }

    public Result SetStockQuantity(int quantity)
    {

        if (Status == ProductStatus.Draft)
        {
            return ProductErrors.SetStockQuantityForDraftProduct;
        }

        if (quantity < 0)
		{
			return ProductErrors.StockQuantityIsLessThanZero;
		}

        StockQuantity = quantity;
        return Result.Success();
    }

    public Result SetPrice(Money newPrice)
    {
        if(newPrice is null)
        {
            return new Error("Product.SetPrice", "Can not set empty price.");
        }

        Price = newPrice;

        return Result.Success();
    }

    public Result AddImage(ProductImage newImage)
    {
        if(_images.Any(image => image.Name == newImage.Name || image.Link == newImage.Link))
        {
            return new Error("Product.AddImage", "Image already exists");
        }

        _images.Add(newImage);
        return Result.Success();
    }
}
