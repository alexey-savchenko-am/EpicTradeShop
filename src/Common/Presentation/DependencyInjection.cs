using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddAutoMapper(config =>
        {
            foreach(var assembly in assemblies)
            {
                config.AddProfile(new AssemblyMappingProfile(assembly));
            }
        });
    }
}
