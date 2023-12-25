using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Config;

internal class ProductConfiguration
    : IEntityTypeConfiguration<ProductAggregate>
{
    public void Configure(EntityTypeBuilder<ProductAggregate> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(product => product.Id);

        builder
            .Property(product => product.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(product => product.Description)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .Property(product => product.Price)
            .HasPrecision(10, 2)
            .HasDefaultValue(null);

        builder
            .Property(product => product.StockQuantity)
            .HasDefaultValue(0);

        builder
            .Property(product => product.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(product => product.Dimensions, dimensions =>
        {
            dimensions
                .Property(dimemsion => dimemsion.Width)
                .IsRequired();
            dimensions
                .Property(dimemsion => dimemsion.Height)
                .IsRequired();
            dimensions
                .Property(dimemsion => dimemsion.Length)
                .IsRequired();
            dimensions
                .Property(dimemsion => dimemsion.Weight)
                .IsRequired();
        });

        builder
            .HasMany(product => product.Categories)
            .WithMany(category => category.Products)
            .UsingEntity(join => join.ToTable("ProductCategories"));
    }
}
