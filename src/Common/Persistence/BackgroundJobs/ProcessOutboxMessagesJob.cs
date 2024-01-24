using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Quartz;
using MassTransit;

namespace Persistence.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly DbContext _dbContextWithOutboxMessages;
    private readonly IPublisher _publisher;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProcessOutboxMessagesJob(
        DbContext dbContextWithOutboxMessages,
        IPublisher publisher,
        IPublishEndpoint publishEndpoint)
    {
        _dbContextWithOutboxMessages = dbContextWithOutboxMessages;
        _publisher = publisher;
        _publishEndpoint = publishEndpoint;
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

        if(messages.Count == 0)
        {
            return;
        }

        var messageTasks = messages.Select(message =>
            Task.Run(() =>
            {
                var messageType = Type.GetType(message.Type);

                var @event = JsonSerializer.Deserialize(message.Content, messageType!);

                message.ProcessedOnUtc = DateTime.UtcNow;   

                if (message.MessageType == MessageType.DomainEvent)
                {
                    return _publisher.Publish(@event!, context.CancellationToken);
                }
                else
                {
                    return _publishEndpoint.Publish(@event!, context.CancellationToken);
                }
            }, context.CancellationToken)
        );

        await Task.WhenAll(messageTasks);
/*
        foreach (var message in messages)
        {
            var messageType = Type.GetType(message.Type);

            if(messageType is null)
            {
                continue;
            }

            var @event = JsonSerializer.Deserialize(message.Content, messageType); 

            if(@event is null)
            {
                continue;
            }

            if(message.MessageType == MessageType.DomainEvent)
            {
                await _publisher.Publish(@event, context.CancellationToken);
            }
            else
            {
                await _publishEndpoint.Publish(@event, context.CancellationToken);
            }

            message.ProcessedOnUtc = DateTime.UtcNow;
        }*/

        await _dbContextWithOutboxMessages.SaveChangesAsync();
    }
}
