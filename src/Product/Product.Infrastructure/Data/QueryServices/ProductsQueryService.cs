using Dapper;
using Product.Application.Abstract;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;
using Product.Domain.Enums;
using SharedKernel.Output;
using System.Data;

namespace Product.Infrastructure.Data.QueryServices;

internal class ProductsQueryService
    : IProductsQueryService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private static Error UnknownProductType(string code, string productType) 
        => new (code, $"Unknown product type {productType}.");

    private static string BaseProductFields(string paramName)
        => $@"
                  {paramName}.Id
                , {paramName}.Type
                , {paramName}.Status
                , {paramName}.StockQuantity
                , {paramName}.ProductName as Name
                , {paramName}.ProductDescription as Description
                , {paramName}.Brand
                , {paramName}.Model
                , {paramName}.ProductPrice as Price
                , {paramName}.Length
                , {paramName}.Width
                , {paramName}.Height
                , {paramName}.Weight
                , {paramName}.Color
                , {paramName}.Material
           ";

    public ProductsQueryService(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> ExistsAsync(BaseProduct.ID productId)
    {
        using var connection = _dbConnectionFactory.GetConnection();
        var productCount = await connection.ExecuteScalarAsync<int>(
            "select count(*) from Products where Id = @id",
            new { id = productId.Key });
        return productCount > 0;
    }

    public async Task<(int totalCount, IEnumerable<ProductResponse> products)> GetProductListAsync(int skip, int take, CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.GetConnection();

        var productCount = await connection.ExecuteScalarAsync<int>("select count(*) from Products");

        if(productCount == 0) 
        {
            return (productCount, new List<ProductResponse>());
        }

        var query = $@"
            select
                {BaseProductFields("p")}
            from Products p
            order by  p.Id
            offset @skip rows
            fetch next @take rows only;
        ";

        var products = await connection.QueryAsync<ProductResponse>(query, new { skip, take });

        return (productCount, products);
    }

    public async Task<Result<ProductResponse?>> GetProductByIdAsync(BaseProduct.ID productId, CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.GetConnection();

        var productTypeString = await connection.ExecuteScalarAsync<string>(
            @"select type from Products where Id = @id", new { id = productId.Key });

        if (string.IsNullOrEmpty(productTypeString))
        {
            return new Error("ProductQueryHandler.GetProductByIdAsync", $"Can not find product with id {productId}.");
        }

        if(!Enum.TryParse<ProductType>(productTypeString, out var productType))
        {
            return UnknownProductType("ProductQueryHandler.GetProductByIdAsync", productTypeString);
        }

        switch(productType)
        {
            case ProductType.Laptop:
                return await GetLaptopProductByIdAsync(connection, productId);

            default:
                return UnknownProductType("ProductQueryHandler.GetProductByIdAsync", productTypeString);
        }
    }

    private async  Task<LaptopProductResponse?> GetLaptopProductByIdAsync(IDbConnection connection, BaseProduct.ID productId)
    {
        var query = $@"
            select
                {BaseProductFields("p")}
                , lp.OperatingSystem
                , lp.ProcessorBrand
                , lp.ProcessorModel
                , lp.ProcessorFrequencyGgc
                , lp.ProcessorCoreCount
                , lp.ProcessorThreadCount
                , lp.GraphicsControllerType
                , lp.GraphicsBrand
                , lp.GraphicsModel
                , lp.VideoMemoryVolumeMb
                , lp.ScreenDiagonalInch
                , lp.ScreenWidthPx
                , lp.ScreenHeightPx
                , lp.RefreshRateGc
                , lp.ViewingAngleDeg
                , lp.RAMType as RamType
                , lp.RAMVolumeGb as RamVolumeGb
                , lp.RAMFrequencyMgc as RamFrequencyMgc
                , lp.RAMIsUpgradeable as RamIsUpgradeable
                , lp.StorageType
                , lp.StorageVolumeGb
                , lp.StorageIsUpgradeable
                , lp.BatteryType
                , lp.BatteryCellCount
                , lp.BatteryCapacityWh
                , lp.BatteryMaxWorktimeHrs
            from Products p
                join LaptopProducts lp on lp.Id = p.Id
            where p.Id = @id and p.Type = 'Laptop'
        ";

        var laptopProduct = await connection.QueryFirstOrDefaultAsync<LaptopProductResponse>(query, new { id = productId.Key });

        return laptopProduct;
    }

    public async Task<Result<byte[]?>> GetProductImageByIdAsync(BaseProduct.ID productId, string imageName, CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.GetConnection();

        var query = $@"
            select 
                pi.Data
            from Products p
                left join ProductImages pi on pi.ProductId = p.Id and pi.Name = @imageName
            where p.Id = @id
        ";

        var imageData = await connection.ExecuteScalarAsync<byte[]?>(query , new { id = productId.Key, imageName });

        return imageData;
    }
}
