﻿using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


namespace Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration => 
            { 
                configuration.RegisterServicesFromAssembly(assembly); 
            });

        services.AddValidatorsFromAssemblies(new[] {assembly});

        return services;
    }
}
