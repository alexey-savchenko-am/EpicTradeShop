using AppCommon.Persistence;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Abstract;

public interface IProductRepository
    : IRepository<ProductAggregate>
{
    Task<ProductAggregate?> GetProductWithFieldsAsync(ProductAggregate.ID productId);
    Task<bool> ExistsAsync(string productName);
    Task<bool> ExistsAsync(ProductAggregate.ID productId);

}
