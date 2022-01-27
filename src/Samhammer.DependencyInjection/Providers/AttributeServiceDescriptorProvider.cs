using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Providers
{
    public class AttributeServiceDescriptorProvider : IServiceDescriptorProvider
    {
        private ILogger<AttributeServiceDescriptorProvider> Logger { get; }

        private DependencyResolverOptions Options { get; }
        
        public AttributeServiceDescriptorProvider(
            ILogger<AttributeServiceDescriptorProvider> logger,
            DependencyResolverOptions options)
        {
            Logger = logger;
            Options = options;
        }

        public IEnumerable<ServiceDescriptor> ResolveServices()
        {
            var assemblies = Options.AssemblyResolvingStrategy.ResolveAssemblies().ToList();
            Logger.LogTrace("Loaded assemblies: {Assemblies}", assemblies.Select(a => a.GetName().Name));

            var types = Options.TypeResolvingStrategy.ResolveTypesByAttribute(assemblies, typeof(DependencyInjectionAttribute));
            Logger.LogTrace("Loaded types with attribute {Attribute}: {Types}", typeof(DependencyInjectionAttribute), types);

            foreach (var type in types)
            {
                var attribute = type.GetTypeInfo().GetCustomAttribute<DependencyInjectionAttribute>(true);
                var descriptors = ResolveService(type, attribute);

                foreach (var descriptor in descriptors)
                {
                    yield return descriptor;
                }
            }
        }

        public IEnumerable<ServiceDescriptor> ResolveService(Type type, DependencyInjectionAttribute attribute)
        {
            var handler = Options.Handlers.ToList().Find(h => h.MatchAttribute(attribute));

            if (handler == null)
            {
                Logger.LogError("Handler for attribute {Attribute} not found", attribute.GetType());
                throw new ArgumentException($"Handler for attribute {attribute.GetType()} not found");
            }

            try
            {
                var descriptors = handler.ResolveServices(type, attribute);
                return descriptors;
            }
            catch (ArgumentException e)
            {
                Logger.LogError("{Message}", e.Message);
                throw;
            }
        }
    }
}
