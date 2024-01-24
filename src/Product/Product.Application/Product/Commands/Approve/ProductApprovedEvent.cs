using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Product.Commands.Approve;

public record ProductApprovedEvent(BaseProduct.ID ProductId);
