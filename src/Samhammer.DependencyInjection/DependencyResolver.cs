using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Providers;

namespace Samhammer.DependencyInjection
{
    public class DependencyResolver
    {
        private ILogger<DependencyResolver> Logger { get; }

        private List<IServiceDescriptorProvider> Providers { get; }

        private readonly object lockObject = new object();

        public DependencyResolver(ILogger<DependencyResolver> logger, IEnumerable<IServiceDescriptorProvider> providers)
        {
            Logger = logger;
            Providers = providers.ToList();
        }

        public void ResolveDependencies(IServiceCollection services)
        {
            Logger.LogInformation("Start service initialization");

            lock (lockObject)
            {
                var serviceDescriptors = ResolveServices();
                RegisterServices(services, serviceDescriptors);
            }

            Logger.LogInformation("Finished service initialization");
        }

        private IEnumerable<ServiceDescriptor> ResolveServices()
        {
            foreach (var provider in Providers)
            {
                var descriptors = provider.ResolveServices();
                foreach (var serviceDescriptor in descriptors)
                {
                    yield return serviceDescriptor;
                }
            }
        }

        private void RegisterServices(IServiceCollection services, IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                services.Add(serviceDescriptor);

                if (Logger.IsEnabled(LogLevel.Debug))
                {
                    Logger.LogDebug(
                        "Added service {Service} with implementation {ServiceImpl} and lifetime {LifeTime}",
                        serviceDescriptor.ServiceType,
                        serviceDescriptor.ImplementationType ?? serviceDescriptor.ImplementationInstance ?? serviceDescriptor.ImplementationFactory,
                        serviceDescriptor.Lifetime);
                }
            }
        }
    }
}
