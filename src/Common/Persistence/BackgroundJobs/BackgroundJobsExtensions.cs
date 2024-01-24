using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Persistence.BackgroundJobs;

public static class BackgroundJobsExtensions
{
    public static void AddQuartzAndOutboxMessagesJob(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey).WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(10).RepeatForever()));
        });

        services.AddQuartzHostedService();
    }
}
