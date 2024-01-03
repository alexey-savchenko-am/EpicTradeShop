using Microsoft.EntityFrameworkCore;
using Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Repositories;

internal class LaptopProductRepository
    : Repository<LaptopProduct>, IProductRepository<LaptopProduct>
{
	public LaptopProductRepository(ProductDbContext productDbContext)
		: base(productDbContext)
	{}


    public Task<LaptopProduct?> GetProductWithFieldsAsync(LaptopProduct.ID productId)
    {
        return Set
            .Where(product => product.Id == productId)
            .Include(product => product.Categories)
            .SingleOrDefaultAsync();
    }
}
