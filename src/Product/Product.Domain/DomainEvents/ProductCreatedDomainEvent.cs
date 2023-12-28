using Product.Domain.Entities.ProductAggregate;
using SharedKernel;

namespace Product.Domain.DomainEvents;

public sealed record ProductCreatedDomainEvent(ProductAggregate.ID productId)
    : IDomainEvent;