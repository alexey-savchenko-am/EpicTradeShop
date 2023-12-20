using AppCommon.Persistence;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Abstract;

public interface IProductRepository
    : IRepository<ProductAggregate>
{
    Task<bool> IsProductExistsAsync(string productName);

}
