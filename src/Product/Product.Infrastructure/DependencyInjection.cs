using AppCommon.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.BackgroundJobs;
using Persistence.EventBus;
using Persistence.Interceptors;
using Product.Application.Abstract;
using Product.Infrastructure.Data;
using Product.Infrastructure.Data.QueryServices;
using Product.Infrastructure.Data.Repositories;
using Quartz;

namespace Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
        services.ConfigureOptions<DatabaseOptionsSetup>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton(provider => 
            provider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

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

        services.AddMassTransit(busConfiguration =>
        {
            busConfiguration.SetKebabCaseEndpointNameFormatter();

            busConfiguration.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
            });
        });

        services.AddOutboxMessageBasedEventBus();
        services.AddQuartzAndOutboxMessagesJob();

        services.AddScoped<ISession, Session>();
        services.AddScoped<IDatabaseInitializer, ProductDbContextSeed>();
        services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
        services.AddScoped<IProductsQueryService, ProductsQueryService>();

        return services;
    }
}
