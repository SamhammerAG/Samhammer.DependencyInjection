using System;
using System.Collections.Generic;
using System.Reflection;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.DependencyInjection.Providers;
using Samhammer.DependencyInjection.Utils;

namespace Samhammer.DependencyInjection.Strategy
{
    public class DefaultTypeResolvingStrategy : ITypeResolvingStrategy
    {
        public IEnumerable<Type> ResolveTypesByAttribute(IEnumerable<Assembly> assemblies, Type attributeType)
        {
            return ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, attributeType);
        }

        public Type GetMatchingInterfaceType(Type implementationType, InjectAttribute injectAttribute)
        {
            var matchingInterfaceName = $"I{implementationType.GetTypeInfo().Name}";
            var serviceType = implementationType.GetInterface(matchingInterfaceName);

            if (serviceType == null)
            {
                throw new ArgumentException($"Class {implementationType} has no matching interface {matchingInterfaceName} defined", nameof(implementationType));
            }

            return serviceType;
        }
    }
}
