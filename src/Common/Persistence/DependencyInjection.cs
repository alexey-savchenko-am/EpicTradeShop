using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Reflection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, Assembly assembly)
    {
        return services;
    }
}
