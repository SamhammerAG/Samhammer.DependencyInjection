using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Providers;

namespace Samhammer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DependencyResolver>();
            serviceCollection.AddSingleton<IServiceDescriptorProvider, AttributeServiceDescriptorProvider>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dependencyResolver = serviceProvider.GetRequiredService<DependencyResolver>();

            dependencyResolver.ResolveDependencies(serviceCollection);
            return serviceCollection;
        }
    }
}
