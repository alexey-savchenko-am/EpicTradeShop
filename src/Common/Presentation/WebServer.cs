﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MinimalApi.Endpoint.Extensions;
using Presentation.Middlewares;
using Serilog;

namespace Presentation;

public abstract class WebServer
{
    protected WebApplicationBuilder Builder { get; }

    protected WebServer(string[] args, string serverName)
    {

        Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

        Log.Information($"Web Server '{serverName}' is starting up {DateTime.UtcNow}.");

        Builder = WebApplication.CreateBuilder(args);

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

        app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

        app.MapEndpoints();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    protected virtual void OnStartingUp(IServiceProvider scopedServices)
    {}

    public void BuildAndRun()
    {
        try
        {
            var app = Builder.Build();
            Configure(app);

            using var scope = app.Services.CreateScope(); 
            OnStartingUp(scope.ServiceProvider);

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
