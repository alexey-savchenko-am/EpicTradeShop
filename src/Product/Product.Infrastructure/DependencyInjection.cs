using AppCommon.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.BackgroundJobs;
using Persistence.Interceptors;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;
using Product.Infrastructure.Data.QueryServices;
using Product.Infrastructure.Data.Repositories;
using Quartz;

namespace Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.ConfigureOptions<DatabaseOptionsSetup>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<DbContext, ProductDbContext>((provider, builder) =>
        {
            var options = provider.GetService<IOptions<DatabaseOptions>>()!.Value;
            var eventsToOutboxMessagesInterceptor = 
                provider.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            builder.UseSqlServer(options.ConnectionString, actions =>
            {
                actions.EnableRetryOnFailure(options.MaxRetryCount);
                actions.CommandTimeout(options.MaxRetryCount);
            });

            builder.AddInterceptors(eventsToOutboxMessagesInterceptor);
            builder.EnableDetailedErrors(options.EnableDetailedErrors);
            builder.EnableSensitiveDataLogging(options.EnableSensitiveDataLogging);
        });

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger => 
                    trigger.ForJob(jobKey).WithSimpleSchedule(schedule => 
                        schedule.WithIntervalInSeconds(10).RepeatForever()));
        });

        services.AddQuartzHostedService();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository<LaptopProduct>, LaptopProductRepository>();
        services.AddScoped<IProductsQueryService, ProductsQueryService>();

        return services;
    }
}
