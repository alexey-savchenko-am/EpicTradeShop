using AppCommon.Cqrs;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Product.Queries.GetById;

public sealed record GetProductByIdQuery(BaseProduct.ID productId)
    : IQuery<ProductResponse>;
