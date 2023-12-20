using Microsoft.EntityFrameworkCore;
using Persistence;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure;

internal class ProductDbContext
    : DbContextWithOutboxMessages
{
    public DbSet<ProductAggregate> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}
