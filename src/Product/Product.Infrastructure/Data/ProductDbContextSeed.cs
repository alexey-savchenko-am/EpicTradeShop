using AppCommon.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Domain.Entities;
using Product.Domain.Entities.ProductAggregate.ConcreteProducts;
using Product.Domain.Enums;
using Serilog;
using SharedKernel.Enums;
using SharedKernel.ValueObjects;
using System;
using System.Text.Json;

namespace Product.Infrastructure.Data;

internal class ProductDbContextSeed
    : IDatabaseInitializer
{
    private readonly Serilog.ILogger _logger;
    private readonly ProductDbContext _productDbContext;

    public ProductDbContextSeed(ProductDbContext productDbContext)
    {
        _logger = Log.ForContext<ProductDbContextSeed>();
        _productDbContext = productDbContext;
    }

    public async Task<bool> InitializeWithTestDataAsync(bool recreateDatabase)
    {
        if (recreateDatabase)
        {
            _productDbContext.Database.EnsureDeleted();

            await _productDbContext.Database.MigrateAsync();
        }


        _logger.Information("Creating seed Products...");

        await _productDbContext.LaptopProducts.AddRangeAsync(SeedLaptopProducts());

        await _productDbContext.SaveChangesAsync();

        _logger.Information("Seed products are persisted successfully.");


        return true;
    }

    private IEnumerable<LaptopProduct> SeedLaptopProducts()
    {
        var product1 = LaptopProduct.Create(
                ProductDetails.Create("Huawei MateBook D 15", "Huawei MateBook D 15"),
                BrandModel.Create(Brand.HUAWEI, "BoDe - WDH9"),
                DimensionsInfo.Create(width: 35.7m, height: 1.6m, length: 22.9m, weight: 1.56m),
                Color.BurgundyRed,
                Material.Aluminum,
                ProductOperatingSystem.None,
                Processor.Create(BrandModel.Create(Brand.Intel, "Core i5 1155G7"), frequencyGgc: 2.5m, coreCount: 4, threadCount: 8),
                Graphics.Create(GraphicsControllerType.Integrated, BrandModel.Create(Brand.Intel, "Iris Xe graphics"), videoMemoryVolumeMb: 0),
                Display.Create(screenDiagonalInch: 15.6m, ScreenResolution.Create(1920, 1080), refreshRateGc: 60, viewingAngleDeg: 178),
                Ram.Create(RamType.DDR4, volumeGb: 8, frequencyMgc: 3200),
                StorageDevice.Create(StorageType.SSD, volumeGb: 512, isUpgradeable: false),
                Battery.Create(BatteryType.LiPol, cellCount: 3, capacityWh: 42, maxWorktimeHrs: 24));

        _logger.Information(JsonSerializer.Serialize(product1.Value));

        var product2 = LaptopProduct.Create(
                ProductDetails.Create("Digma Pro Sprint M DN15P3-8CXW02", "Digma Pro Sprint M DN15P3-8CXW02"),
                BrandModel.Create(Brand.Digma, "Sprint M"),
                DimensionsInfo.Create(width: 35.8m, height: 1.6m, length: 23.9m, weight: 1.82m),
                Color.Black,
                Material.MetalPlastic,
                ProductOperatingSystem.Windows,
                Processor.Create(BrandModel.Create(Brand.Intel, "Core i3 1115G4"), frequencyGgc: 3.0m, coreCount: 2, threadCount: 4),
                Graphics.Create(GraphicsControllerType.Integrated, BrandModel.Create(Brand.Intel, "UHD Graphics"), videoMemoryVolumeMb: 0),
                Display.Create(screenDiagonalInch: 15.6m, ScreenResolution.Create(1920, 1080), refreshRateGc: 60, viewingAngleDeg: 178),
                Ram.Create(RamType.DDR4, volumeGb: 8, frequencyMgc: 3200),
                StorageDevice.Create(StorageType.SSD, volumeGb: 256, isUpgradeable: false),
                Battery.Create(BatteryType.LiPol, cellCount: 3, capacityWh: 51, maxWorktimeHrs: 24));

        _logger.Information(JsonSerializer.Serialize(product2.Value));

        var product3 = LaptopProduct.Create(
                ProductDetails.Create("Huawei MateBook D 15", "Huawei MateBook D 15"),
                BrandModel.Create(Brand.HUAWEI, "BoDe - WDH9"),
                DimensionsInfo.Create(width: 35.7m, height: 1.6m, length: 22.9m, weight: 1.56m),
                Color.BurgundyRed,
                Material.Aluminum,
                ProductOperatingSystem.None,
                Processor.Create(BrandModel.Create(Brand.Intel, "Core i5 1155G7"), frequencyGgc: 2.5m, coreCount: 4, threadCount: 8),
                Graphics.Create(GraphicsControllerType.Integrated, BrandModel.Create(Brand.Intel, "Iris Xe graphics"), videoMemoryVolumeMb: 0),
                Display.Create(screenDiagonalInch: 15.6m, ScreenResolution.Create(1920, 1080), refreshRateGc: 60, viewingAngleDeg: 178),
                Ram.Create(RamType.DDR4, volumeGb: 8, frequencyMgc: 1),
                StorageDevice.Create(StorageType.SSD, volumeGb: 512, isUpgradeable: false),
                Battery.Create(BatteryType.LiPol, cellCount: 3, capacityWh: 42, maxWorktimeHrs: 24));

        _logger.Information(JsonSerializer.Serialize(product3.Value));

        yield return product1;
        yield return product2;
        yield return product3;
    }
}
