using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence;

namespace Product.Infrastructure.Data.Config;

internal class ProductConfiguration
    : EntityWithPrimaryKeyConfiguration<Domain.Entities.ProductAggregate.BaseProduct>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.ProductAggregate.BaseProduct> builder)
    {
        base.Configure(builder);

        builder.ToTable("Products");

        builder
            .Property(product => product.Type)
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(product => product.StockQuantity)
            .HasDefaultValue(0);

        builder
            .Property(product => product.Status)
            .HasConversion<string>()
            .IsRequired();


        builder.OwnsOne(product => product.ProductDetails, details =>
        {
            details.Property(pd => pd.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();


            details.Property(pd => pd.Description)
                .HasColumnName("ProductDescription")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.OwnsOne(product => product.BrandModel, brandModel =>
        {
            brandModel.Property(bm => bm.Brand)
                .HasColumnName("Brand")
                .HasConversion<string>()
                .IsRequired();
                
            brandModel.Property(bm => bm.Model)
                .HasColumnName("Model")
                .HasMaxLength(70)
                .IsRequired();
        });

        builder.OwnsOne(product => product.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("ProductPrice")
                .HasPrecision(12, 2)
                .HasDefaultValue(null);

            price.Property(p => p.Currency)
                .HasColumnName("ProductPriceCurrency")
                .HasMaxLength(3)
                .HasConversion<string>()
                .HasDefaultValue(null);
            
        });

        builder.OwnsOne(product => product.Dimensions, dimensions =>
        {
            dimensions
                .Property(dimemsion => dimemsion.Width)
                .HasColumnName("Width")
                .IsRequired();
            dimensions
                .Property(dimemsion => dimemsion.Height)
                .HasColumnName("Height")
                .IsRequired();
            dimensions
                .Property(dimemsion => dimemsion.Length)
                .HasColumnName("Length")
                .IsRequired();
            dimensions
                .Property(dimemsion => dimemsion.Weight)
                .HasColumnName("Weight")
                .IsRequired();
        });

        builder
            .HasMany(product => product.Categories)
            .WithMany(category => category.Products)
            .UsingEntity(join => 
                join.ToTable("ProductCategories"));
    }
}