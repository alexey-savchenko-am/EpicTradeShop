using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;

namespace Product.Application.Abstract;

public interface IProductsQueryService
{
    Task<bool> ExistsAsync(BaseProduct.ID productId);

    Task<(int totalCount, IEnumerable<ProductResponse> products)> GetProductListAsync(int skip, int take, CancellationToken cancellationToken);

    Task<Result<ProductResponse?>> GetProductByIdAsync(BaseProduct.ID productId, CancellationToken cancellationToken);

    Task<Result<byte[]?>> GetProductImageByIdAsync(BaseProduct.ID productId, string imageName, CancellationToken cancellationToken);
}
