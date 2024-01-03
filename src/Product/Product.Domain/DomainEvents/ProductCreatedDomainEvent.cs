using Product.Domain.Entities.ProductAggregate;
using SharedKernel;

namespace Product.Domain.DomainEvents;

public sealed record ProductCreatedDomainEvent(Entities.ProductAggregate.BaseProduct.ID productId)
    : IDomainEvent;