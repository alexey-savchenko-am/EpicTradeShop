using MediatR;

namespace SharedKernel;

public interface IDomainEventHandler<TEvent>
    : INotificationHandler<TEvent>
    where TEvent: IDomainEvent
{
}
