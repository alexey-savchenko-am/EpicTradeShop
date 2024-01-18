using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Config;

public sealed class ProductImageConfiguration
        : EntityWithPrimaryKeyConfiguration<ProductImage>
{
    public override void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        base.Configure(builder);

        builder.ToTable("ProductImages");

        builder.Property(image => image.Name)
            .IsRequired();

        builder.Property(image => image.Link)
            .IsRequired();

        builder.Property(image => image.Data)
            .IsRequired();

        builder.HasOne(image => image.Product)
          .WithMany(product => product.Images)
          .HasForeignKey(image => image.ProductId);
    }
}
