using Microsoft.EntityFrameworkCore;
using Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Repositories;

internal class ProductRepository<TProduct>
    : Repository<TProduct>, IProductRepository<TProduct>
    where TProduct : BaseProduct
{
	public ProductRepository(ProductDbContext productDbContext)
		: base(productDbContext)
	{}

    public Task<TProduct?> GetProductWithFieldsAsync(BaseProduct.ID productId, CancellationToken cancellationToken)
    {
        return Set
            .Where(product => product.Id == productId)
            .Include(product => product.Categories)
            .Include(product => product.Images)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
