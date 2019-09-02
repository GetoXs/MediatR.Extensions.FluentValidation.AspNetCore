using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using System.Linq;

namespace MediatR.Extensions.FluentValidation.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.RegisterAllTypesGeneric(typeof(IPipelineBehavior<,>), assemblies, lifetime);

            services.AddValidatorsFromAssemblies(assemblies, lifetime);
        }

        public static void RegisterAllTypesGeneric(this IServiceCollection services, Type t, IEnumerable<Assembly> assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var genericType = t;
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType)));

            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(t, type, lifetime));
            }
        }
    }
}
