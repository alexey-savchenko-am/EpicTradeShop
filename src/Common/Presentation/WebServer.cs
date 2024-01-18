
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Presentation.Endpoints;
using Presentation.Middlewares;
using Presentation.Swagger;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            loggerConfiguration
                .WriteTo.Console()
                .ReadFrom.Configuration(hostBuilderContext.Configuration);
        });

        ConfigureServices(Builder.Services);
    }

    protected void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddVersionedEndpoints();
        services.AddEndpointsApiExplorer();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
        });


        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });


        ConfigureSpecificServices(services);
    }

    protected abstract void ConfigureSpecificServices(IServiceCollection services);

    protected abstract ApiVersionSet ConfigureApiVersions(WebApplication app);

    protected virtual void Configure(WebApplication app)
    {
        app.MapVersionedEndpoints(ConfigureApiVersions(app));
        
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseSerilogRequestLogging();

        app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }

    protected abstract Task OnStartingUpAsync(IServiceProvider scopedServices, bool isDevelopment);

    public async Task BuildAndRunAsync()
    {
        try
        {
            var app = Builder.Build();
            Configure(app);

            using var scope = app.Services.CreateScope(); 
            await OnStartingUpAsync(scope.ServiceProvider, app.Environment.IsDevelopment()).ConfigureAwait(false);

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
