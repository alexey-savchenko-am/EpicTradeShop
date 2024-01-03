using Product.Domain.DomainEvents;
using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities.ProductAggregate;

public class BaseProduct
    : AggregateRoot
{
	private List<Category> _categories = new();

    public ProductType Type { get; }
    public ProductDetails ProductDetails { get; private set; }
    public BrandModel BrandModel { get; private set; }
    public Money? Price { get; private set; }
	public int StockQuantity { get; private set; }
    public DimensionsInfo Dimensions { get; private set; }
    public ProductStatus Status { get; private set; }
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

    protected BaseProduct()
        : base(new ID(Guid.NewGuid()))
    {}

	protected BaseProduct(
        ProductType type, 
        ProductDetails productDetails, 
        BrandModel brandModel, 
        DimensionsInfo dimensions)
		: base(new ID(Guid.NewGuid()))
	{
        Type = type;
        ProductDetails = productDetails;
        BrandModel = brandModel;
        Dimensions = dimensions;
		StockQuantity = 0;
		Status = ProductStatus.Draft;
	}

	public static Result<BaseProduct> Create(
        ProductType type,
        ProductDetails productDetails,
        BrandModel brandModel,
        DimensionsInfo dimensions)
	{
		var product = new BaseProduct(type, productDetails, brandModel, dimensions);
        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));
        return product;
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
        BrandModel? brandModel, ProductDetails? productDetails, DimensionsInfo? dimentions)
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
}
