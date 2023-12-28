using AppCommon.Cqrs;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Product.Commands;

public sealed record ApproveProductCommand
    : ICommand
{
    public ProductAggregate.ID ProductId { get; }
    public decimal? ProductPrice { get; }

    public ApproveProductCommand(ProductAggregate.ID productId, decimal? productPrice)
	{
        ProductId = productId;
        ProductPrice = productPrice;
    }
}
