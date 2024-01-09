using AppCommon.Cqrs;
using Product.Application.Abstract;
using Product.Application.Responses;
using SharedKernel.Output;

namespace Product.Application.Product.Queries.GetAllPaginated;

public sealed class GetAllProductsPaginatedQueryHandler
    : IQueryHandler<GetAllProductsPaginatedQuery, PagedResponse<ProductResponse>>
{
    private readonly IProductsQueryService _productsQueryHandler;

    public GetAllProductsPaginatedQueryHandler(IProductsQueryService productsQueryHandler)
    {
        _productsQueryHandler = productsQueryHandler;
    }

    public async Task<Result<PagedResponse<ProductResponse>>> Handle(GetAllProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * request.ProductsPerPage;
        var take = request.ProductsPerPage;

        var productsWithCount = await _productsQueryHandler.GetProductListAsync(skip, take, cancellationToken);

        var result = new PagedResponse<ProductResponse>(
            productsWithCount.products.ToList(), 
            request.Page, 
            request.ProductsPerPage,
            productsWithCount.totalCount / request.ProductsPerPage + 1);

        return result;
    }
}
