using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace MediatR.Extensions.FluentValidation.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.Add(new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), lifetime));

            services.AddValidatorsFromAssemblies(assemblies, lifetime);
        }
    }
}
