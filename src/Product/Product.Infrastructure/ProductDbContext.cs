using Microsoft.EntityFrameworkCore;
using Persistence;
using Product.Domain.Entities.ProductAggregate;
using Product.Domain.Entities.ProductAggregate.ConcreteProducts;

namespace Product.Infrastructure;

internal class ProductDbContext
    : DbContextWithOutboxMessages
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<BaseProduct> Products { get; set; }
    public DbSet<LaptopProduct> LaptopProducts { get; set; }

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
