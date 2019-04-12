using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions;
using Samhammer.DependencyInjection.Utils;

namespace Samhammer.DependencyInjection.Providers
{
    public class AttributeServiceDescriptorProvider : IServiceDescriptorProvider
    {
        private ILogger<AttributeServiceDescriptorProvider> Logger { get; }

        public AttributeServiceDescriptorProvider(ILogger<AttributeServiceDescriptorProvider> logger)
        {
            Logger = logger;
        }

        public IEnumerable<ServiceDescriptor> ResolveServices()
        {
            var assemblies = AssemblyUtils.LoadAllAssembliesOfProject();
            Logger.LogTrace("Loaded assemblies: {Assemblies}.", assemblies.Select(a => a.GetName().Name));

            var types = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, typeof(InjectAttribute));
            Logger.LogTrace("Loaded types with attribute {Attribute}: {Types}.", typeof(InjectAttribute), types);

            foreach (var type in types)
            {
                var descriptors = ResolveService(type);
                foreach (var descriptor in descriptors)
                {
                    yield return descriptor;
                }
            }

            var factoryTypes = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, typeof(FactoryAttribute));
            Logger.LogTrace("Loaded types with attribute {Attribute}: {Types}.", typeof(FactoryAttribute), types);

            foreach (var type in factoryTypes)
            {
                var descriptors = ResolveServiceFactory(type);
                foreach (var descriptor in descriptors)
                {
                    yield return descriptor;
                }
            }
        }

        public IEnumerable<ServiceDescriptor> ResolveService(Type implementationType)
        {
            var injectAttribute = implementationType.GetTypeInfo().GetCustomAttribute<InjectAttribute>(true);
            var serviceTypes = implementationType.GetInterfaces().ToList();

            if (serviceTypes.Count == 0)
            {
                Logger.LogWarning("Implementation {ServiceImpl} has no interfaces defined", implementationType);
            }

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
                yield return serviceDescriptor;
            }
        }

        public IEnumerable<ServiceDescriptor> ResolveServiceFactory(Type factoryType)
        {
            var factoryAttribute = factoryType.GetTypeInfo().GetCustomAttribute<FactoryAttribute>(true);
            var factoryMethods = GetFactoryMethods(factoryType);

            foreach (var factoryMethod in factoryMethods)
            {
                var serviceType = factoryMethod.ReturnType;
                object FactoryFunc(IServiceProvider provider) => factoryMethod.Invoke(null, new object[] { provider });

                var serviceDescriptor = new ServiceDescriptor(serviceType, FactoryFunc, factoryAttribute.LifeTime);
                yield return serviceDescriptor;
            }
        }

        public List<MethodInfo> GetFactoryMethods(Type factoryType)
        {
            var methods = factoryType.GetMethods(BindingFlags.Static | BindingFlags.Public).ToList();

            methods = methods
                .Where(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(IServiceProvider))
                .ToList();

            if (methods.Count == 0)
            {
                Logger.LogWarning("Factory {Factory} has no factory methods defined", factoryType);
            }

            return methods;
        }
    }
}
