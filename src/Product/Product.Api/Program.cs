using Product.Application;
using Product.Infrastructure;
using Presentation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Asp.Versioning.Builder;
using Asp.Versioning;
using AppCommon.Persistence;

await new ProductServer(args).BuildAndRunAsync();

public class ProductServer : WebServer
{
    public ProductServer(string[] args)
        : base(args, "ProductService")
    {
    }

    protected override async Task OnStartingUpAsync(IServiceProvider scopedServices, bool isDevelopment)
    {
        /*var dbContext = scopedServices.GetRequiredService<DbContext>();
        if(dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
        */

        if (isDevelopment)
        {
            var databaseInitializer = scopedServices.GetRequiredService<IDatabaseInitializer>();
            await databaseInitializer.InitializeWithTestDataAsync(recreateDatabase: true);
        }
    }

    protected override void ConfigureSpecificServices(IServiceCollection services)
    {
        services
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation(
                Assembly.GetExecutingAssembly(), 
                typeof(Product.Application.DependencyInjection).Assembly);
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

