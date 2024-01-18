using AppCommon.Cqrs;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Product.Queries.GetImage;

public sealed record GetImageQuery(
    BaseProduct.ID ProductId,
    string ImageNameWithExtension)
    : IQuery<GetProductImageResponse>;