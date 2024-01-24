using AppCommon.Cqrs;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.ValueObjects;

namespace Product.Application.Product.Commands.Approve;

public sealed record ApproveProductCommand(BaseProduct.ID productId, Money? price)
    : ICommand;
