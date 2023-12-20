
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MinimalApi.Endpoint.Extensions;
using Serilog;

namespace Presentation;

public abstract class Server
{

    protected WebApplicationBuilder Builder { get; }

    protected Server(string[] args, string serverName)
    {

        Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

        Log.Information($"Web Server '{serverName}' is starting up {DateTime.UtcNow}.");

        Builder = WebApplication.CreateBuilder();

        Builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
        {
            loggerConfiguration.WriteTo.Console().ReadFrom.Configuration(hostBuilderContext.Configuration);
        });

        ConfigureServices(Builder.Services);
    }

    protected void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpoints();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });

        ConfigureSpecificServices(services);
    }

    protected abstract void ConfigureSpecificServices(IServiceCollection services);

    protected virtual void Configure(WebApplication app)
    {
        app.UseRouting();

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseSerilogRequestLogging();

        app.MapEndpoints();
    }

    public void BuildAndRun()
    {
        try
        {
            var app = Builder.Build();
            Configure(app);
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"When starting the web server, an exception occurred {DateTime.UtcNow}.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
