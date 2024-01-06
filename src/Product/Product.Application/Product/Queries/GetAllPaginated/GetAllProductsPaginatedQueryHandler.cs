using AppCommon.Cqrs;
using Dapper;
using Product.Application.Abstract;
using Product.Application.Responses;
using SharedKernel.Output;

namespace Product.Application.Product.Queries.GetAllPaginated;

public sealed class GetAllProductsPaginatedQueryHandler
    : IQueryHandler<GetAllProductsPaginatedQuery, PaginatedResult<ProductResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetAllProductsPaginatedQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PaginatedResult<ProductResponse>>> Handle(GetAllProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * request.ProductsPerPage;
        var take = request.ProductsPerPage;

        var productList = await GetProductListAsync(skip, take, cancellationToken);

        var productItems = new List<ProductResponse>(productList);

        var result = new PaginatedResult<ProductResponse>(productItems!, request.Page, request.ProductsPerPage, 10);

        return result;
    }

    private async Task<IEnumerable<ProductResponse>> GetProductListAsync(int skip, int take, CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.GetConnection();

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

        var products = await connection.QueryAsync<ProductResponse>(query, new {skip, take });

        return products;
    }
}
