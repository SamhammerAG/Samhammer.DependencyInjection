using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions.Attributes;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Utils;

namespace Samhammer.DependencyInjection.Providers
{
    public class AttributeServiceDescriptorProvider : IServiceDescriptorProvider
    {
        private ILogger<AttributeServiceDescriptorProvider> Logger { get; }

        private IAssemblyResolvingStrategy AssemblyResolvingStrategy { get; }

        private List<IAttributeServiceDescriptorHandler> Handlers { get; }

        public AttributeServiceDescriptorProvider(
            ILogger<AttributeServiceDescriptorProvider> logger,
            IEnumerable<IAttributeServiceDescriptorHandler> handlers,
            IAssemblyResolvingStrategy assemblyResolvingStrategy)
        {
            Logger = logger;
            AssemblyResolvingStrategy = assemblyResolvingStrategy;
            Handlers = handlers.ToList();
        }

        public IEnumerable<ServiceDescriptor> ResolveServices()
        {
            var assemblies = AssemblyResolvingStrategy.ResolveAssemblies().ToList();
            Logger.LogTrace("Loaded assemblies: {Assemblies}.", assemblies.Select(a => a.GetName().Name));

            var types = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, typeof(DependencyInjectionAttribute));
            Logger.LogTrace("Loaded types with attribute {Attribute}: {Types}.", typeof(DependencyInjectionAttribute), types);

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
            var handler = Handlers.Find(h => h.MatchAttribute(attribute));

            if (handler == null)
            {
                Logger.LogError("Handler for attribute {Attribute} not found.", attribute.GetType());
                throw new ArgumentException("Handler for attribute not found");
            }

            var descriptors = handler.ResolveServices(type, attribute);
            return descriptors;
        }
    }
}
