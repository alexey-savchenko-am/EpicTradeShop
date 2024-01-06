using AppCommon.Cqrs;
using Product.Application.Responses;

namespace Product.Application.Product.Queries.GetAllPaginated;

public sealed record GetAllProductsPaginatedQuery(int Page, int ProductsPerPage)
    : IQuery<PaginatedResult<ProductResponse>>;
