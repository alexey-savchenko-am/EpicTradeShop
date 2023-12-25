using AppCommon.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Product.Application.Abstract;
using Product.Infrastructure.Data.Repositories;

namespace Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddDbContext<DbContext, ProductDbContext>((provider, builder) =>
        {
            var options = provider.GetService<IOptions<DatabaseOptions>>()!.Value;

            builder.UseSqlServer(options.ConnectionString, actions =>
            {
                actions.EnableRetryOnFailure(options.MaxRetryCount);
                actions.CommandTimeout(options.MaxRetryCount);
            });

            builder.EnableDetailedErrors(options.EnableDetailedErrors);
            builder.EnableSensitiveDataLogging(options.EnableSensitiveDataLogging);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
