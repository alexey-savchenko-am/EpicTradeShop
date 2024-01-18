using Product.Domain.Enums;
using SharedKernel;

namespace Product.Domain.DomainEvents;

public sealed record ProductStatusChangedDomainEvent(
    Entities.ProductAggregate.BaseProduct.ID productId, 
    ProductStatus newStatus) : IDomainEvent;