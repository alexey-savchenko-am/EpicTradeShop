using AppCommon.Persistence;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Abstract;

public interface IProductRepository<TProduct>
    : IRepository<TProduct>
    where TProduct: BaseProduct
{
    Task<TProduct?> GetProductWithFieldsAsync(BaseProduct.ID productId, CancellationToken cancellationToken);
}
