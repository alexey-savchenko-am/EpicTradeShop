using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Config;

internal class CategoryConfiguration
    : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("ProductCategories");
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name).HasMaxLength(128);

    }
}
