﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Providers;
using Samhammer.DependencyInjection.Strategy;

namespace Samhammer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        [Obsolete]
        public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection, IAssemblyResolvingStrategy assemblyResolvingStrategy)
        {
            serviceCollection.ResolveDependencies(options => options.SetStrategy(assemblyResolvingStrategy));
            return serviceCollection;
        }

        public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection, Action<DependencyResolverOptions> customizeOptions = null)
        {
            var options = BuildDefaultOptions();
            customizeOptions?.Invoke(options);

            var logger = options.BuildLogger<DependencyResolver>();
            var resolver = new DependencyResolver(logger, options.Providers);

            resolver.ResolveDependencies(serviceCollection);

            return serviceCollection;
        }

        private static DependencyResolverOptions BuildDefaultOptions()
        {
            var options = new DependencyResolverOptions
            {
                LoggerFactory = new NullLoggerFactory(),
                AssemblyResolvingStrategy = new DefaultAssemblyResolvingStrategy(),
            };

            options.AddAttributeHandler<FactoryServiceDescriptorHandler>(logger => new FactoryServiceDescriptorHandler(logger));
            options.AddAttributeHandler<InjectAllServiceDescriptorHandler>(logger => new InjectAllServiceDescriptorHandler(logger));
            options.AddAttributeHandler<InjectAsServiceDescriptorHandler>(logger => new InjectAsServiceDescriptorHandler(logger));
            options.AddAttributeHandler<InjectMatchingServiceDescriptorHandler>(logger => new InjectMatchingServiceDescriptorHandler(logger));
            options.AddProvider<AttributeServiceDescriptorProvider>((logger, strategy) => new AttributeServiceDescriptorProvider(logger, options.Handlers, strategy));
            
            return options;
        }
    }
}
