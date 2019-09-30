using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Providers;
using Samhammer.DependencyInjection.Utils;

namespace Samhammer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection, IAssemblyResolvingStrategy assemblyResolvingStrategy = null)
        {
            serviceCollection.AddSingleton<DependencyResolver>();

            if (assemblyResolvingStrategy == null)
            {
                serviceCollection.AddSingleton<IAssemblyResolvingStrategy, DefaultAssemblyResolvingStrategy>();
            }
            else
            {
                serviceCollection.AddSingleton(assemblyResolvingStrategy);
            }

            serviceCollection.AddSingleton<IServiceDescriptorProvider, AttributeServiceDescriptorProvider>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, FactoryServiceDescriptorHandler>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, InjectMatchingServiceDescriptorHandler>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, InjectAllServiceDescriptorHandler>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, InjectAsServiceDescriptorHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dependencyResolver = serviceProvider.GetRequiredService<DependencyResolver>();

            dependencyResolver.ResolveDependencies(serviceCollection);
            return serviceCollection;
        }
    }
}
