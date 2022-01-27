using System;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Providers;

namespace Samhammer.DependencyInjection
{
    public static class OptionsExtensions
    {
        public static DependencyResolverOptions AddProvider<T>(this DependencyResolverOptions options, Func<ILogger<T>, DependencyResolverOptions, T> createProvider) where T : IServiceDescriptorProvider
        {
            var logger = options.BuildLogger<T>();
            var provider = createProvider(logger, options);

            options.Providers.Add(provider);
            return options;
        }

        public static DependencyResolverOptions AddAttributeHandler<T>(this DependencyResolverOptions options, Func<ILogger<T>, T> createHandler) where T : IAttributeServiceDescriptorHandler
        {
            var logger = options.BuildLogger<T>();
            var handler = createHandler(logger);

            options.Handlers.Add(handler);
            return options;
        }

        public static DependencyResolverOptions SetAssemblyStrategy(this DependencyResolverOptions options, IAssemblyResolvingStrategy strategy)
        {
            options.AssemblyResolvingStrategy = strategy;
            return options;
        }
        
        public static DependencyResolverOptions SetTypeStrategy(this DependencyResolverOptions options, ITypeResolvingStrategy strategy)
        {
            options.TypeResolvingStrategy = strategy;
            return options;
        }

        public static DependencyResolverOptions SetLogging(this DependencyResolverOptions options, ILoggerFactory factory)
        {
            options.LoggerFactory = factory;
            return options;
        }

        public static ILogger<T> BuildLogger<T>(this DependencyResolverOptions options)
        {
            return options.LoggerFactory.CreateLogger<T>();
        }
    }
}
