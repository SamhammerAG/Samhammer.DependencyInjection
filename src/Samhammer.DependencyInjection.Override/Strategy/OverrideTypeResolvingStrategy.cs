using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.DependencyInjection.Override.Attributes;
using Samhammer.DependencyInjection.Providers;
using Samhammer.DependencyInjection.Utils;

namespace Samhammer.DependencyInjection.Override.Strategy
{
    public class OverrideTypeResolvingStrategy : ITypeResolvingStrategy
    {
        private string ConfigurationName { get; }
        
        public OverrideTypeResolvingStrategy(string configurationName)
        {
            ConfigurationName = configurationName;
        }

        public IEnumerable<Type> ResolveTypesByAttribute(IEnumerable<Assembly> assemblies, Type attributeType)
        {
            var types = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, attributeType);
            var typeTuples = types.Select(type => (Type: type, OverrideAttribute: GetOverrideAttribute(type)));
            
            return typeTuples
                .Where(t => t.OverrideAttribute == null || string.Equals(t.OverrideAttribute.ConfigurationName, ConfigurationName))
                .OrderBy(t => t.OverrideAttribute == null ? 0 : 1)
                .Select(t => t.Type);
        }

        public Type GetMatchingInterfaceType(Type implementationType, InjectAttribute injectAttribute)
        {
            var overrideAttribute = GetOverrideAttribute(implementationType);

            var matchingInterfaceName = overrideAttribute == null
                ? $"I{implementationType.GetTypeInfo().Name}"
                : $"I{implementationType.BaseType.GetTypeInfo().Name}";
            
            var serviceType = implementationType.GetInterface(matchingInterfaceName);
            if (serviceType == null)
            {
                throw new ArgumentException($"Class {implementationType} has no matching interface {matchingInterfaceName} defined", nameof(implementationType));
            }

            return serviceType;
        }

        private static OverrideAttribute GetOverrideAttribute(Type type, bool inherit = true)
        {
            return type.GetTypeInfo().GetCustomAttribute<OverrideAttribute>(inherit);
        }
    }
}
