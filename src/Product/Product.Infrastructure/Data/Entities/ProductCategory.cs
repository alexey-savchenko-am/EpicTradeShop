using Product.Domain.Entities.ProductAggregate;
using SharedKernel;

namespace Product.Infrastructure.Data.Entities;

internal class ProductCategory
{
    public ProductAggregate Product { get; set; }
    public Category Category { get; set; }
    public ProductAggregate.ID ProductId { get; set; }
    public Category.ID CategoryId { get; set; }
}
