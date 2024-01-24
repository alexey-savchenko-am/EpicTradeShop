using Stock.Application;
using Stock.Infrastructure;
using Presentation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Asp.Versioning.Builder;
using Asp.Versioning;
using AppCommon.Persistence;

await new StockService(args).BuildAndRunAsync();

public class StockService : WebServer
{
    public StockService(string[] args)
        : base(args, "StockService")
    {
    }

    protected override async Task OnStartingUpAsync(IServiceProvider scopedServices, bool isDevelopment)
    {
        if (isDevelopment)
        {
            var databaseInitializer = scopedServices.GetRequiredService<IDatabaseInitializer>();
            await databaseInitializer.InitializeWithTestDataAsync(recreateDatabase: true);
        }
    }

    protected override void ConfigureSpecificServices(
        IServiceCollection services, 
        IConfiguration configuration)
    {/*
        services
            //.AddApplication()
            .AddInfrastructure(configuration)
            .AddPresentation(
                Assembly.GetExecutingAssembly(),
                typeof(Product.Application.DependencyInjection).Assembly);*/
    }

    protected override ApiVersionSet ConfigureApiVersions(WebApplication app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();
    }
}

