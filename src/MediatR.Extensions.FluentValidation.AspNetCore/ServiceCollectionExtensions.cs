using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MediatR.Extensions.FluentValidation.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidation(
            this IServiceCollection services, 
            IEnumerable<Assembly> assemblies, 
            ServiceLifetime lifetime = ServiceLifetime.Transient,
            Func<AssemblyScanner.AssemblyScanResult, bool> filter = null,
            bool includeInternalTypes = false
            )
        {
            services.Add(new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), lifetime));

            services.AddValidatorsFromAssemblies(assemblies, lifetime, filter, includeInternalTypes);
            
            return services;
        }
    }
}
