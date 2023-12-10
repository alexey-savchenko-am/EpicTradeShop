using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public class Product
    : AggregateRoot
{
	private List<Category> _categories = new();

	public string Name { get; private set; }
	public string? Description { get; private set; }
	public decimal Price { get; private set; }
	public int StockQuantity { get; private set; }
	public ProductStatus Status { get; private set; }
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

	private Product(Guid id, string name, decimal price, int stockQuantity)
		: base(id)
	{
		this.Name = name;
		this.Price = price;
		this.StockQuantity = stockQuantity;
		this.Status = stockQuantity > 0 ? ProductStatus.InStock : ProductStatus.OutOfStock;
	}

	public static Result<Product> Create(string name, decimal price, int stockQuantity)
	{
		return new Product(Guid.NewGuid(), name, price, stockQuantity);
	}
}
