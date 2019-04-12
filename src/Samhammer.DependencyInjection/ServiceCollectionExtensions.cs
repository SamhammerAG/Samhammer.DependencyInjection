using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Providers;

namespace Samhammer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DependencyResolver>();
            serviceCollection.AddSingleton<IServiceDescriptorProvider, AttributeServiceDescriptorProvider>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, FactoryServiceDescriptorHandler>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, InjectServiceDescriptorHandler>();
            serviceCollection.AddSingleton<IAttributeServiceDescriptorHandler, InjectAsServiceDescriptorHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dependencyResolver = serviceProvider.GetRequiredService<DependencyResolver>();

            dependencyResolver.ResolveDependencies(serviceCollection);
            return serviceCollection;
        }
    }
}
