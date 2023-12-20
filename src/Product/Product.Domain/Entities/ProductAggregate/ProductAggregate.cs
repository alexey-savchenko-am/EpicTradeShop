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
	public decimal Price { get; private set; }
	public int StockQuantity { get; private set; }
    public DimensionsInfo Dimensions { get; private set; }
    public ProductStatus Status { get; private set; }
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

	private ProductAggregate(string name, decimal price, DimensionsInfo dimensions, string? description)
		: base(new ID())
	{
        Name = name;
		Price = price;
        Dimensions = dimensions;
		StockQuantity = 0;
		Description = description;
		Status = ProductStatus.Draft;
	}

	public static Result<ProductAggregate> Create(
        string name, 
        decimal price,
        DimensionsInfo dimensions,
        string? description = null)
	{
		return new ProductAggregate(name, price, dimensions, description);
	}

	public Result AddCategories(IEnumerable<string> categories)
	{
        foreach(var category in categories)
        {
            _categories.Add(Category.Create(category));
        }
        return Result.Success();
    }

    public Result ChangeStatus(ProductStatus newStatus)
    {
        if(this.Status == ProductStatus.Draft && newStatus == ProductStatus.Suspended)
        {
            return new Error("Product.ChangeStatus", "Attempted to suspend draft product");
        }

        Status = newStatus;
        return Result.Success();
    }

    public Result SetStockQuantity(int quantity)
    {

        if (this.Status == ProductStatus.Draft)
        {
            return new Error("Product.SetStockQuantity", "Attempt to set stock quantity of draft product.");
        }

        if (quantity < 0)
		{
			return new Error("Product.SetStockQuantity", "Stock quantity is less than zero.");
		}

        this.StockQuantity = quantity;
        this.Status = this.StockQuantity > 0 ? ProductStatus.Active : ProductStatus.OutOfStock;

        return Result.Success();
    }

    public Result SetProductPrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            return new Error("Product.SetProductPrice", "New price is less than zero.");
        }

        this.Price = newPrice;

        return Result.Success();
    }
}
