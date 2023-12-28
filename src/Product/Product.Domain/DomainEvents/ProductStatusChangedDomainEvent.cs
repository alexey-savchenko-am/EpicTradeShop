using Product.Domain.Entities.ProductAggregate;
using SharedKernel;

namespace Product.Domain.DomainEvents;

public sealed record ProductStatusChangedDomainEvent(
    ProductAggregate.ID productId, 
    ProductStatus newStatus) : IDomainEvent;