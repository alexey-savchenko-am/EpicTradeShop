using Product.Domain.DomainEvents;
using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities.ProductAggregate;

public class ProductAggregate
    : AggregateRoot
{
	private List<Category> _categories = new();

    public string Name { get; private set; }
	public string? Description { get; private set; }
	public decimal? Price { get; private set; }
	public int StockQuantity { get; private set; }
    public DimensionsInfo Dimensions { get; private set; }
    public ProductStatus Status { get; private set; }
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

    private ProductAggregate()
        : base(new ID())
    {}

	private ProductAggregate(string name, DimensionsInfo dimensions, string? description)
		: base(new ID())
	{
        Name = name;
        Dimensions = dimensions;
		StockQuantity = 0;
		Description = description;
		Status = ProductStatus.Draft;
	}

	public static Result<ProductAggregate> Create(
        string name, 
        DimensionsInfo dimensions,
        string? description = null)
	{
		var product = new ProductAggregate(name, dimensions, description);
        RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));
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

    public Result Edit(string? productName = null, string? description = null, DimensionsInfo? dimentions = null)
    {
        if(Status > ProductStatus.Draft) 
        {
            return ProductErrors.CanNotEditNonDraftProduct;
        }

        if(productName is not null)
        {
            Name = productName;
        }
        if(description is not null) 
        { 
            Description = description;
        }
        if(dimentions is not null)
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

    public Result SetProductPrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            return ProductErrors.ProductPriceIsLessThanOrEqualToZero;
        }

        Price = newPrice;

        return Result.Success();
    }
}
