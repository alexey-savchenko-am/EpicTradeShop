using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities.ProductAggregate;
using Product.Domain.Entities.ProductAggregate.ConcreteProducts;

namespace Product.Infrastructure.Data.Config;

internal class LaptopProductConfiguration
    : IEntityTypeConfiguration<LaptopProduct>
{
    public void Configure(EntityTypeBuilder<LaptopProduct> builder)
    {
        builder.ToTable("LaptopProducts");
        builder.HasBaseType<BaseProduct>();

        builder.Property(product => product.OperatingSystem)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(product => product.Display, display =>
        {
            display.WithOwner();

            display.Property(d => d.ScreenDiagonalInch)
                .HasColumnName("ScreenDiagonalInch")
                .HasPrecision(10, 2)
                .IsRequired();

            display.Property(d => d.RefreshRateGc)
                .HasColumnName("RefreshRateGc")
                .IsRequired();

            display.Property(d => d.ViewingAngleDeg)
                .HasColumnName("ViewingAngleDeg")
                .IsRequired();

            display.OwnsOne(d => d.ScreenResolution, sr =>
            {
                sr.WithOwner();

                sr.Property(sr => sr.Width)
                    .HasColumnName("ScreenWidthPx")
                    .IsRequired();

                sr.Property(sr => sr.Height)
                    .HasColumnName("ScreenHeightPx")
                    .IsRequired();
            });
        });

        builder.OwnsOne(product => product.Graphics, graphics =>
        {
            graphics.Property(g => g.GraphicsControllerType)
            .HasColumnName("GraphicsControllerType")
            .HasConversion<string>()
            .IsRequired();


            graphics.Property(g => g.VideoMemoryVolumeMb)
                .HasColumnName("VideoMemoryVolumeMb")
                .IsRequired();

            graphics.OwnsOne(g => g.BrandModel, bm =>
            {
                bm.Property(bm => bm.Brand)
                    .HasColumnName("GraphicsBrand")
                    .HasConversion<string>()
                        .IsRequired();

                bm.Property(bm => bm.Model)
                    .HasColumnName("GraphicsModel")
                    .HasMaxLength(70)
                    .IsRequired();
            });
        });

        builder.OwnsOne(product => product.Processor, processor =>
        {
            processor.Property(p => p.FrequencyGgc)
                .HasColumnName("ProcessorFrequencyGgc")
                .HasPrecision(10, 2)
                .IsRequired();

            processor.Property(p => p.CoreCount)
                .HasColumnName("ProcessorCoreCount")
                .IsRequired();

            processor.Property(p => p.ThreadCount)
                .HasColumnName("ProcessorThreadCount")
                .IsRequired();

            processor.OwnsOne(p => p.BrandModel, bm =>
            {
                bm.Property(bm => bm.Brand)
                    .HasColumnName("ProcessorBrand")
                    .HasConversion<string>()
                        .IsRequired();

                bm.Property(bm => bm.Model)
                    .HasColumnName("ProcessorModel")
                    .HasMaxLength(70)
                    .IsRequired();
            });
        });

        builder.OwnsOne(product => product.Ram, ram =>
        {
            ram.Property(r => r.Type)
                .HasColumnName("RAMType")
                .HasConversion<string>()
                .IsRequired();

            ram.Property(r => r.VolumeGb)
                .HasColumnName("RAMVolumeGb")
                .IsRequired();

            ram.Property(r => r.FrequencyMgc)
                .HasColumnName("RAMFrequencyMgc")
                .HasPrecision(10, 2);

            ram.Property(r => r.IsUpgradeable)
                .HasColumnName("RAMIsUpgradeable");
        });

        builder.OwnsOne(product => product.Storage, storage =>
        {
            storage.Property(s => s.StorageType)
                .HasColumnName("StorageType")
                .HasConversion<string>()
                .IsRequired();

            storage.Property(s => s.VolumeGb)
                .HasColumnName("StorageVolumeGb")
                .IsRequired();

            storage.Property(s => s.IsUpgradeable)
            .HasColumnName("StorageIsUpgradeable")
            .HasDefaultValue(false)
            .IsRequired();
        });


        builder.OwnsOne(product => product.Battery, battery =>
        {
            battery.Property(b => b.BatteryType)
                .HasColumnName("BatteryType")
                .HasConversion<string>()
                .IsRequired();

            battery.Property(b => b.CellCount)
                .HasColumnName("BatteryCellCount")
               .IsRequired();

            battery.Property(b => b.CapacityWh)
               .HasColumnName("BatteryCapacityWh")
               .IsRequired();

            battery.Property(b => b.MaxWorktimeHrs)
               .HasColumnName("BatteryMaxWorktimeHrs")
               .IsRequired(false);
        });
    }
}
