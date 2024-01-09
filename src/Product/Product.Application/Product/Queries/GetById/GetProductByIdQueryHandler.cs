using AppCommon.Cqrs;
using Product.Application.Abstract;
using Product.Application.Responses;
using SharedKernel.Output;

namespace Product.Application.Product.Queries.GetById;

public sealed class GetProductByIdQueryHandler
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductsQueryService _productQueryService;

    public GetProductByIdQueryHandler(IProductsQueryService productQueryService)
    {
        _productQueryService = productQueryService;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productResult = await _productQueryService.GetProductByIdAsync(request.productId, cancellationToken);
        if (productResult.IsFailure)
        {
            return new Error("GetProductByIdQueryHandler.Handle", "Can not get product by id.", productResult.Error);
        }
        return productResult.Value;
    }
}
