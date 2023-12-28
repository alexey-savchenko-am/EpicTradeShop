using Product.Domain.DomainEvents;
using SharedKernel;

namespace Product.Application.Product.Events;

internal sealed class ProductStatusChangedDomainEventHandler
    : IDomainEventHandler<ProductStatusChangedDomainEvent>
{
    public Task Handle(ProductStatusChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
