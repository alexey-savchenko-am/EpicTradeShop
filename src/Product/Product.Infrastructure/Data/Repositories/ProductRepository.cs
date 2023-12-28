using Microsoft.EntityFrameworkCore;
using Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Repositories;

internal class ProductRepository
    : Repository<ProductAggregate>, IProductRepository
{
	public ProductRepository(ProductDbContext productDbContext)
		: base(productDbContext)
	{}


    public Task<ProductAggregate?> GetProductWithFieldsAsync(ProductAggregate.ID productId)
    {
        return Set
            .Where(product => product.Id == productId)
            .Include(product => product.Categories)
            .SingleOrDefaultAsync();
    }

    public Task<bool> ExistsAsync(string productName)
    {
        return Set.AnyAsync(product => product.Name == productName);
    }

    public Task<bool> ExistsAsync(ProductAggregate.ID productId)
    {
        return Set.AnyAsync(product => product.Id == productId);
    }
}
