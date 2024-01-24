using AppCommon.EventBus;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedKernel;
using System.Text.Json;

namespace Persistence.EventBus;

public class OutboxMessageBasedEventBus
    : IEventBus
{
    private readonly DbContext _dbContext;

    public OutboxMessageBasedEventBus(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) 
        where T : class
    {
        if(_dbContext is not DbContextWithOutboxMessages dbContextWithOutboxMessages)
        {
            return;
        }

        await dbContextWithOutboxMessages
            .Set<OutboxMessage>()
            .AddAsync(new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccuredOnUtc = DateTime.UtcNow,
                Type = typeof(T).AssemblyQualifiedName!,
                MessageType = MessageType.IntegrationEvent,
                Content = JsonSerializer.Serialize(message)
            }, cancellationToken);
    }
}
