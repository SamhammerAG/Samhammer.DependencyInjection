using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            serviceCollection.ResolveDependencies(options => options.SetAssemblyStrategy(assemblyResolvingStrategy));
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
                TypeResolvingStrategy = new DefaultTypeResolvingStrategy(),
            };

            options.AddAttributeHandler<FactoryServiceDescriptorHandler>(logger => new FactoryServiceDescriptorHandler());
            options.AddAttributeHandler<InjectAllServiceDescriptorHandler>(logger => new InjectAllServiceDescriptorHandler());
            options.AddAttributeHandler<InjectAsServiceDescriptorHandler>(logger => new InjectAsServiceDescriptorHandler());
            options.AddAttributeHandler<InjectMatchingServiceDescriptorHandler>(logger => new InjectMatchingServiceDescriptorHandler(options));
            options.AddProvider<AttributeServiceDescriptorProvider>((logger, o) => new AttributeServiceDescriptorProvider(logger, o));

            return options;
        }

        public static IServiceCollection Decorate<TInterface, TDecorator>(this IServiceCollection services)
            where TInterface : class
            where TDecorator : class, TInterface
        {
            // grab the existing registration
            var wrappedDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(TInterface));

            // check its valid
            if (wrappedDescriptor == null)
            {
                throw new InvalidOperationException($"{typeof(TInterface).Name} is not registered");
            }

            // create the object factory for our decorator type,
            // specifying that we will supply TInterface explicitly
            var objectFactory = ActivatorUtilities.CreateFactory(typeof(TDecorator), new[] { typeof(TInterface) });

            // replace the existing registration with one
            // that passes an instance of the existing registration
            // to the object factory for the decorator
            services.Replace(ServiceDescriptor.Describe(
                typeof(TInterface),
                sp => (TInterface)objectFactory(sp, new[] { sp.CreateInstance(wrappedDescriptor) }),
                wrappedDescriptor.Lifetime));

            return services;
        }

        private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationFactory != null)
            {
                return descriptor.ImplementationFactory(services);
            }

            if (descriptor.ImplementationType != null)
            {
                return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
            }

            throw new InvalidOperationException("ServiceDescriptor is invalid");
        }
    }
}
