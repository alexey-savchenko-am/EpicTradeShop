using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Config;

internal class CategoryConfiguration
    : EntityWithPrimaryKeyConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categories");

        builder.Property(category => category.Name).HasMaxLength(128);

    }
}
