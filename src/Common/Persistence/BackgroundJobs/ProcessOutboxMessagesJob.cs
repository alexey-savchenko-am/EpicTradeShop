using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using SharedKernel;

namespace Persistence.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly DbContext _dbContextWithOutboxMessages;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(
        DbContext dbContextWithOutboxMessages,
        IPublisher publisher)
    {
        _dbContextWithOutboxMessages = dbContextWithOutboxMessages;
        _publisher = publisher;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        if (_dbContextWithOutboxMessages is not DbContextWithOutboxMessages)
        {
            return;
        }

        var messages = await _dbContextWithOutboxMessages
            .Set<OutboxMessage>()
            .Where(message => message.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (var message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content); 

            if(domainEvent is null)
            {
                continue;
            }

            await _publisher.Publish(domainEvent, context.CancellationToken);
            message.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContextWithOutboxMessages.SaveChangesAsync();
    }
}
