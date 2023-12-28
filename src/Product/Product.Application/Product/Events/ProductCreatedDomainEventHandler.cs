using Product.Domain.DomainEvents;
using SharedKernel;

namespace Product.Application.Product.Events;

internal sealed class ProductCreatedDomainEventHandler
    : IDomainEventHandler<ProductCreatedDomainEvent>
{
    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
