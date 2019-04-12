using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Providers.Attribute
{
    public abstract class AttributeServiceDescriptorHandler<T> : IAttributeServiceDescriptorHandler where T : DependencyInjectionAttribute
    {
        public bool MatchAttribute(DependencyInjectionAttribute attribute)
        {
            return attribute.GetType() == typeof(T);
        }

        public IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, DependencyInjectionAttribute attribute)
        {
            return ResolveServices(implementationType, (T)attribute);
        }

        public abstract IEnumerable<ServiceDescriptor> ResolveServices(Type implementationType, T attribute);
    }
}
