using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Providers.Attribute
{
    public class FactoryServiceDescriptorHandler : IAttributeServiceDescriptorHandler
    {
        private ILogger<FactoryServiceDescriptorHandler> Logger { get; }

        public FactoryServiceDescriptorHandler(ILogger<FactoryServiceDescriptorHandler> logger)
        {
            Logger = logger;
        }

        public bool MatchAttribute(DependencyInjectionAttribute attribute)
        {
            return attribute.GetType() == typeof(FactoryAttribute);
        }

        public IEnumerable<ServiceDescriptor> ResolveServices(Type factoryType, DependencyInjectionAttribute attribute)
        {
            var factoryAttribute = (FactoryAttribute) attribute;
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
