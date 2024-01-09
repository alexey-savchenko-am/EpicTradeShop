using Dapper;
using Product.Application.Abstract;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using System.Data;

namespace Product.Infrastructure.Data.QueryServices;

internal class ProductsQueryService
    : IProductsQueryService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private static Error UnknownProductType(string code, string productType) 
        => new (code, $"Unknown product type {productType}.");

    public ProductsQueryService(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<(int totalCount, IEnumerable<ProductResponse> products)> GetProductListAsync(int skip, int take, CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.GetConnection();

        var productCount = await connection.ExecuteScalarAsync<int>("select count(*) from Products");

        if(productCount == 0) 
        {
            return (productCount, new List<ProductResponse>());
        }

        var query = @"
            select
                p.Id
                , p.Type
                , p.Status
                , p.StockQuantity
                , p.ProductName as Name
                , p.ProductDescription as Description
                , p.Brand
                , p.Model
                , p.ProductPrice as Price
                , p.Length
                , p.Width
                , p.Height
                , p.Weight
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
        var query = @"
            select
                p.Id
                , p.Type
                , p.Status
                , p.StockQuantity
                , p.ProductName as Name
                , p.ProductDescription as Description
                , p.Brand
                , p.Model
                , p.ProductPrice as Price
                , p.Length
                , p.Width
                , p.Height
                , p.Weight
                , lp.ProcessorBrand
                , lp.ProcessorModel
            from Products p
                join LaptopProducts lp on lp.Id = p.Id
            where p.Id = @id
        ";

        var laptopProduct = await connection.QueryFirstOrDefaultAsync<LaptopProductResponse>(query, new { id = productId.Key });

        return laptopProduct;
    }



}
