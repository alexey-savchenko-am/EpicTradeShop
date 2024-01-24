
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel;
using System.Text.Json;

namespace Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if(dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        if(dbContext is DbContextWithOutboxMessages dbContextWithOutboxMessages)
        {
            var outboxMessages = dbContextWithOutboxMessages.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(aggregateRoot =>
                {
                    var domainEvents = aggregateRoot.GetDomainEvents();
                    aggregateRoot.ClearDomainEvents();
                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = DateTime.UtcNow,
                    Type = domainEvent.GetType().AssemblyQualifiedName!,
                    MessageType = MessageType.DomainEvent,
                    Content = JsonSerializer.Serialize(domainEvent, domainEvent.GetType())
                })
                .ToList();

            dbContextWithOutboxMessages.Set<OutboxMessage>().AddRange(outboxMessages);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
