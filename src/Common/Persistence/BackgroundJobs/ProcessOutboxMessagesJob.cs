using Quartz;

namespace Persistence.BackgroundJobs;

[DisallowConcurrentExecution]
internal class ProcessOutboxMessagesJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
