using Product.Application;
using Product.Infrastructure;
using Presentation;


var server = new ProductServer(args);
server.BuildAndRun();

public class ProductServer : Server
{
    public ProductServer(string[] args)
        : base(args, "ProductService")
    {
    }

    protected override void ConfigureSpecificServices(IServiceCollection services)
    {
        services
            .AddApplication()
            .AddInfrastructure();
    }
}

