using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Handlers
{
    public class FactoryServiceDescriptorHandler : AttributeServiceDescriptorHandler<FactoryAttribute>
    {
        public override IEnumerable<ServiceDescriptor> ResolveServices(Type factoryType, FactoryAttribute factoryAttribute)
        {
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
                throw new ArgumentException($"Class {factoryType} has no factory methods defined", nameof(factoryType));
            }

            return methods;
        }
    }
}
