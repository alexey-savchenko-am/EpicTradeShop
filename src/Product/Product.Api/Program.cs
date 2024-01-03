using Product.Application;
using Product.Infrastructure;
using Presentation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Policy;

new ProductServer(args).BuildAndRun();

public class ProductServer : WebServer
{
    public ProductServer(string[] args)
        : base(args, "ProductService")
    {
    }

    protected override void OnStartingUp(IServiceProvider scopedServices)
    {
        var dbContext = scopedServices.GetRequiredService<DbContext>();
        if(dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
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
}

