using Product.Domain.Entities.ProductAggregate;
using Product.Domain.Enums;
using SharedKernel;

namespace Product.Domain.DomainEvents;

public sealed record ProductStatusChangedDomainEvent(
    BaseProduct.ID productId, 
    ProductStatus newStatus) : IDomainEvent;