using Product.Domain.Entities.ProductAggregate;
using SharedKernel;

namespace Product.Domain.DomainEvents;

public sealed record ProductStatusChangedDomainEvent(
    Entities.ProductAggregate.BaseProduct.ID productId, 
    ProductStatus newStatus) : IDomainEvent;