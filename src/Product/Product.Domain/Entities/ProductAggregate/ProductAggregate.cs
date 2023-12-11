using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public class ProductAggregate
    : AggregateRoot
{
	private List<Category> _categories = new();

    public string Sku { get; private set; }
    public string Name { get; private set; }
	public string? Description { get; private set; }
	public decimal Price { get; private set; }
	public int StockQuantity { get; private set; }
	public ProductStatus Status { get; private set; }
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

	private ProductAggregate(string sku, string name, decimal price, string? description)
		: base(new ID())
	{
        this.Sku = sku;
        this.Name = name;
		this.Price = price;
		this.StockQuantity = 0;
		this.Description = description;
		this.Status = ProductStatus.Draft;
	}

	public static Result<ProductAggregate> Create(string sku, string name, decimal price, string? description = null)
	{
		return new ProductAggregate(sku, name, price, description);
	}

	public Result AddCategory(Category category)
	{
        this._categories.Add(category);
        return Result.Success();
    }

    public Result ChangeStatus(ProductStatus newStatus)
    {
		if(newStatus == this.Status)
		{
            return Result.Success();
        }

        if(this.Status == ProductStatus.Draft && newStatus == ProductStatus.Suspended)
        {
            return new Error("Product.ChangeStatus", "Attempted to suspend draft product");
        }

        this.Status = newStatus;
        return Result.Success();
    }

    public Result DecreaseStockQuantity(int quantity)
    {
		var newQuantity = this.StockQuantity - quantity;

        if (newQuantity < 0)
		{
			return new Error("Product.DecreaseStockQuantity", "Stock quantity is less than zero.");
		}

        if(this.Status == ProductStatus.Draft)
        {
            return new Error("Product.DecreaseStockQuantity", "Attempt to decrease stock quantity of draft product.");
        }

        this.StockQuantity = newQuantity;
        this.Status = this.StockQuantity > 0 ? ProductStatus.Active : ProductStatus.OutOfStock;

        return Result.Success();
    }

    public Result IncreaseStockQuantity(int quantity)
    {
        if (this.Status == ProductStatus.Draft)
        {
            return new Error("Product.IncreaseStockQuantity", "Attempt to increase stock quantity of draft product.");
        }

        this.StockQuantity += quantity;
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
