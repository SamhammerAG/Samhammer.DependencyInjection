using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DependencyResolver>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dependencyResolver = serviceProvider.GetRequiredService<DependencyResolver>();

            dependencyResolver.ResolveDependencies(serviceCollection);
            return serviceCollection;
        }
    }
}
