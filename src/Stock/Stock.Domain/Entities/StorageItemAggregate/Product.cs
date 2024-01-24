using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;
using Stock.Domain.Enums;

namespace Stock.Domain.Entities.StockItemAggregate;

public sealed class Product
    : GuidKeyEntity
{
    public ProductStatus Status { get; private set; }
    public DimensionsInfo Dimensions { get; }

    #pragma warning disable CS8618
    private Product() : base(new ID(Guid.NewGuid())){ }

    private Product(Product.ID productId, DimensionsInfo dimensions)
        : base(productId)
    {
        Status = ProductStatus.Draft;
        Dimensions = dimensions;
    }

    public Result<Product> Create(ID productId, DimensionsInfo dimensions)
    {
        return new Product(productId, dimensions);
    }

}
